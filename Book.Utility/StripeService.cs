using Book.Models;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic;
using Stripe;
using Stripe.BillingPortal;
using Stripe.Checkout;
using Stripe.FinancialConnections;
using System.Collections.Generic;

namespace Book.Utility;

public class StripeService
{
    private readonly string _stripeApiKey;

    public StripeService(IConfiguration config)
    {
        _stripeApiKey = config.GetSection("StripeSecrets").GetValue<string>("StripeTestKey") ?? "StripeKeyNotFound";
        StripeConfiguration.ApiKey = _stripeApiKey;
    }

    public Stripe.Checkout.Session CreateCheckoutSession(IEnumerable<ShoppingCartItem> shoppingItemList, string returnUrl)
    {
        var options = new Stripe.Checkout.SessionCreateOptions
        {
            PaymentMethodTypes = new List<string> {"card"},
            LineItems = shoppingItemList.Select(item => new SessionLineItemOptions{

                PriceData = new SessionLineItemPriceDataOptions
                {
                    UnitAmount = (long)(item.Price * 100),
                    Currency = "usd",
                    ProductData = new SessionLineItemPriceDataProductDataOptions
                    {
                        Name = item.Product.Title,
                    }            
                    
                },
                Quantity = item.Amount,
            }).ToList(),
            Mode = "payment",
            SuccessUrl = returnUrl + "?success=true",
            CancelUrl = returnUrl + "?success=false",
        };

        var service = new Stripe.Checkout.SessionService();
        var session = service.Create(options);

        return session;
    }

    public PaymentIntent ConfirmPayment(string paymentIntentId)
    {
        var service = new PaymentIntentService();
        var paymentIntent = service.Confirm(paymentIntentId);

        return paymentIntent;
    }
}