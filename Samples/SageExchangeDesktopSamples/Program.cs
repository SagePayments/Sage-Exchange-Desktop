using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using clSedApi;
using SpsXmlCL.Client;

namespace SageExchangeDesktopSamples
{
    partial class Program
    {
        const double paymentAmount = 1.00;
        const string merchantId = "999999999997";
        const string merchantKey = "K3QD6YWyhfD";
        const string applicationId = "DEMO";
        const string languageId = "EN";

        static void Main(string[] args)
        {
            Console.WriteLine("Press any key to attempt transaction.");
            Console.ReadLine();

            var sedClient = new ModuleClient();
            
            var payment = new Request_v1.PaymentType();

            payment.Merchant = new Request_v1.MerchantType
            {
                MerchantID = merchantId,
                MerchantKey = merchantKey
            };

            payment.TransactionBase = new Request_v1.TransactionBaseType
            {
                TransactionType = "11",
                TransactionID = Guid.NewGuid().ToString(),
                Reference1 = "Invoice " + (new Random()).Next(0, 100).ToString(),
                Amount = paymentAmount
            };

            payment.Customer = new Request_v1.PersonType
            {
                Name = new Request_v1.NameType
                {
                    FirstName = "John",
                    MI = "Q",
                    LastName = "Developer"
                }
                // ... Address = new Request_v1.AddressType...
            };

            Request_v1 sedRequest = new Request_v1();

            sedRequest.Payments = new Request_v1.PaymentType[1];
            sedRequest.Payments[0] = payment;

            sedRequest.Application = new Request_v1.ApplicationType
            {
                ApplicationID = applicationId,
                LanguageID = languageId
            };

            
            ModuleResponse sedResponse = (ModuleResponse)sedClient.GetResponse(sedRequest.ToXml());

            Console.WriteLine("Status Code: " + sedResponse.GetStatusCode().ToString());
            Console.WriteLine("Status Desc: " + sedResponse.GetStatusDescription().ToString());
            Console.WriteLine();

            // uncomment to print out the raw xml response
            //Console.WriteLine("Response Length: " + sedResponse.GetResponseLength());
            //Console.WriteLine("Response Text: " + sedResponse.GetResponseText());
            //Console.WriteLine();

            Response_v1 paymentResponse = Response_v1.FromXml(sedResponse.GetResponseText());
            Console.WriteLine("Response Message: " + paymentResponse.PaymentResponses[0].Response.ResponseMessage);
            Console.WriteLine("Transacion Reference: " + paymentResponse.PaymentResponses[0].TransactionResponse.VANReference);
            Console.WriteLine();

            Console.WriteLine("Press any key to quit.");
            Console.ReadLine();

            
        }

    }
}
