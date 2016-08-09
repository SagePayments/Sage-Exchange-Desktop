using SpsXmlCL.Client;
using System;

namespace SageExchangeDesktopSamples
{
    class RequestFactory
    {
        static public Request_v1 GetBaseRequest()
        {
            // Request_v1 is the base of all SED requests.
            var SedRequest = new Request_v1();

            // Configure the application.
            SedRequest.Application = new Request_v1.ApplicationType
            {
                ApplicationID = Config.applicationId, // You'll be assigned an ApplicationID before you go live.
                LanguageID = Config.languageId // "EN" for English, etc.
            };

            return SedRequest;
        }

        static public Request_v1 GetPaymentRequest()
        {
            var SedRequest = GetBaseRequest();

            // We'll build our payment, and then add it to the base request.
            var PaymentRequest = new Request_v1.PaymentType();

            // Set the merchant account to use for the payment.
            PaymentRequest.Merchant = new Request_v1.MerchantType
            {
                MerchantID = Config.merchantId,
                MerchantKey = Config.merchantKey
            };

            // Configuring the transaction itself.
            PaymentRequest.TransactionBase = new Request_v1.TransactionBaseType
            {
                TransactionType = "", // we'll set this in the individual samples
                TransactionID = Config.transactionId = Guid.NewGuid().ToString(), // you can use this value to poll for the transaction status
                Reference1 = "Invoice " + (new Random()).Next(0, 100).ToString(), // an order number... a random invoice number works here
                Amount = Config.paymentAmount
            };

            // Adding the customer's name and billing address.
            PaymentRequest.Customer = new Request_v1.PersonType
            {
                Name = new Request_v1.NameType
                {
                    FirstName = "John",
                    MI = "Q",
                    LastName = "Developer"
                },
                Address = new Request_v1.AddressType
                {
                    AddressLine1 = "123 Address St",
                    City = "Denver",
                    State = "CO",
                    ZipCode = "12345"
                }
            };

            // You can send multiple payment requests at once, but most situations only require one.
            SedRequest.Payments = new Request_v1.PaymentType[1];
            SedRequest.Payments[0] = PaymentRequest;
            
            return SedRequest;
        }

    }
}
