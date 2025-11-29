using Microsoft.AspNetCore.Http;
using Silverhand.DAL.DTO.Requests;
using Silverhand.DAL.DTO.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silverhand.BLL.Services.Interface
{
    public interface IPaymentService
    {
        Task<PaymentResponse> StartAsync(PaymentRequest request, Guid userId, HttpRequest httpRequest);
        Task<bool> HandleSuccessAsync(Guid subscriptionId);
    }
}
