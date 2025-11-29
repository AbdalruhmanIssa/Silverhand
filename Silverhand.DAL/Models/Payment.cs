using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silverhand.DAL.Models
{
    public class Payment
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid UserId { get; set; }
        public Guid SubscriptionId { get; set; }

        public string Provider { get; set; } = "Stripe";
        public string ProviderPaymentId { get; set; } = null!;

        public string Currency { get; set; } = "usd";
        public long AmountMinor { get; set; }

        public PaymentStatus Status { get; set; } = PaymentStatus.Pending;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
    }
    public enum PaymentStatus
    {
        Pending = 0,
        Succeeded = 1,
        Failed = 2
    }
}
