using Microsoft.AspNetCore.Http;
using Silverhand.BLL.Services.Interface;
using Silverhand.DAL.DTO.Requests;
using Silverhand.DAL.DTO.Responses;
using Silverhand.DAL.Models;
using Silverhand.DAL.Repository.Repositories;
using Stripe;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silverhand.BLL.Services.Classes
{
    public class PaymentService : IPaymentService
    {
        private readonly IPlanRepository _planRepo;
        private readonly ISubscriptionRepository _subscriptionRepo;
        private readonly IPaymentRepository _paymentRepo;

        public PaymentService(
            IPlanRepository planRepo,
            ISubscriptionRepository subscriptionRepo,
            IPaymentRepository paymentRepo)
        {
            _planRepo = planRepo;
            _subscriptionRepo = subscriptionRepo;
            _paymentRepo = paymentRepo;
        }

        public async Task<PaymentResponse> StartAsync(
            PaymentRequest request,
            Guid userId,
            HttpRequest httpRequest)
        {
            var plan = await _planRepo.GetByIdAsync(request.PlanId);
            if (plan == null || !plan.IsActive)
            {
                return new PaymentResponse { Success = false, Message = "Plan not found." };
            }

            var subscription = new DAL.Models.Subscription
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                PlanId = plan.Id,
                Status = SubscriptionStatus.Trialing,
                PeriodStartUtc = DateTime.UtcNow,
                PeriodEndUtc = DateTime.UtcNow,
                CreatedAt = DateTime.UtcNow,
                Provider = "Stripe"
            };

            await _subscriptionRepo.AddAsync(subscription);

            var domain = $"{httpRequest.Scheme}://{httpRequest.Host}";

            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                Mode = "payment",
                SuccessUrl = $"{domain}/api/payments/success/{subscription.Id}",
                CancelUrl = $"{domain}/payment/cancel",
                LineItems = new List<SessionLineItemOptions>
            {
                new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        Currency = plan.Currency.ToLower(),
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = plan.Name,
                            Description = $"Subscription - {plan.Interval}"
                        },
                        UnitAmount = (long)(plan.Price * 100)
                    },
                    Quantity = 1
                }
            }
            };

            var sessionService = new SessionService();
            var session = await sessionService.CreateAsync(options);

            var payment = new Payment
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                SubscriptionId = subscription.Id,
                Provider = "Stripe",
                ProviderPaymentId = session.Id,
                Currency = plan.Currency,
                AmountMinor = (long)(plan.Price * 100),
                Status = PaymentStatus.Pending,
                CreatedAt = DateTime.UtcNow
            };

            await _paymentRepo.AddAsync(payment);

            return new PaymentResponse
            {
                Success = true,
                Message = "Stripe session created.",
                Url = session.Url,
                PaymentId = session.Id
            };
        }

        public async Task<bool> HandleSuccessAsync(Guid subscriptionId)
        {
            var subscription = await _subscriptionRepo.GetByIdAsync(subscriptionId);
            if (subscription == null) return false;

            var payment = await _paymentRepo.GetBySubscriptionIdAsync(subscriptionId);
            if (payment == null) return false;

            var plan = await _planRepo.GetByIdAsync(subscription.PlanId);
            if (plan == null) return false;

            subscription.Status = SubscriptionStatus.Active;
            subscription.PeriodStartUtc = DateTime.UtcNow;
            subscription.PeriodEndUtc = plan.Interval == BillingInterval.Monthly
    ? DateTime.UtcNow.AddMonths(1)
    : DateTime.UtcNow.AddYears(1);



            subscription.UpdatedAt = DateTime.UtcNow;
            await _subscriptionRepo.UpdateAsync(subscription);

            payment.Status = PaymentStatus.Succeeded;
            payment.UpdatedAt = DateTime.UtcNow;
            await _paymentRepo.UpdateAsync(payment);

            return true;
        }
    }
}