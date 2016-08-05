using System;
using clSedApi;
using SpsXmlCL.Client;

namespace SageExchangeDesktopSamples
{
    partial class Samples
    {
        static public void Sale()
        {
            Console.WriteLine("Press any key to run Sale.");
            Console.ReadLine();
            Console.WriteLine("Running Sale transaction.");
            Console.WriteLine();
            // -----------------------------------------------

            // The SED API exposes a client object that manages communication
            // between your application and Sage Exchange Desktop itself.
            var ApiClient = new ModuleClient();

            // The SpsXmlCL library can be used to construct request objects.
            var PaymentRequest = new Request_v1.PaymentType();

            // Set the merchant account for the request.
            PaymentRequest.Merchant = new Request_v1.MerchantType
            {
                MerchantID = Config.merchantId,
                MerchantKey = Config.merchantKey
            };

            // Configuring the transaction itself.
            PaymentRequest.TransactionBase = new Request_v1.TransactionBaseType
            {
                TransactionType = "11", // 11 is a Sale -- see the complete documentation for a complete list.
                TransactionID = Config.transactionId = Guid.NewGuid().ToString(), // you can use this value to poll for the transaction status
                Reference1 = "Invoice " + (new Random()).Next(0, 100).ToString(), // an order number... a random invoice number works for this demo
                Amount = Config.paymentAmount // the payment amount
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

            // Request_v1 is the root element for all SED requests.
            Request_v1 SedRequest = new Request_v1();

            // You can send multiple payment requests at once, but most situations only require one.
            SedRequest.Payments = new Request_v1.PaymentType[1];
            SedRequest.Payments[0] = PaymentRequest;

            // Add some information about the application.
            SedRequest.Application = new Request_v1.ApplicationType
            {
                ApplicationID = Config.applicationId, // You'll be assigned an ApplicationID before you go live.
                LanguageID = Config.languageId // "EN" for English, etc.
            };

            // Convert the request object to an XML string.
            string XmlRequest = SedRequest.ToXml();

            // Note that many integrators prefer to construct the XML string directly,
            // instead of building the request object first and serializing. For example,
            // if all your requests are basic sales that only vary by dollar amount and 
            // the order number, it may be simpler to use a string literal + String.Format().

            // See the "XML Messaging" document for more on SED requests.

            // But anyway, however you decide to generate your XML, pass it into the client object.
            // This is the point at which the UI pops up to collect the payment information.
            ModuleResponse ApiResponse = (ModuleResponse)ApiClient.GetResponse(SedRequest.ToXml());

            // All requests through the SED API return a status code and description:
            int StatusCode = ApiResponse.GetStatusCode();
            string StatusDesc = ApiResponse.GetStatusDescription();

            Console.WriteLine("Status Code: " + StatusCode.ToString());
            Console.WriteLine("Status Desc: " + StatusDesc);

            if (StatusCode == 0)
            {
                // Status Code "0" indicates a successful request. Note that this does NOT 
                // mean the transaction was approved; a transaction that declined due to 
                // insufficient funds, for instance, is still a successful -request-, despite
                // not being a successful -transaction-.

                // Get the gateway response from the response object...
                string XmlResponse = ApiResponse.GetResponseText();

                // ... and deserialize it, if you want:
                Response_v1 SedResponse = Response_v1.FromXml(ApiResponse.GetResponseText());

                // ResponseIndicator "A" means the transaction was approved.
                Console.WriteLine("Approved: " + (SedResponse.PaymentResponses[0].Response.ResponseIndicator == "A" ? "Yes" : "No"));

                // We'll save the transaction reference for later. It's used for voids, refunds, etc.
                Config.vanReference = SedResponse.PaymentResponses[0].TransactionResponse.VANReference;
            }
        }
    }
}
