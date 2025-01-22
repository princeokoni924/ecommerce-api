using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Redis;

namespace Core.Contract
{
    public interface IPaymentService
    {
        Task<ShoppingCart?>CreateOrUpdatePaymentItent(string cartId);
        Task<string>RefundPayment(string paymentIntentId);
    }
}