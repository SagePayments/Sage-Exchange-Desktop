using System;
using clSedApi;
using SpsXmlCL.Client;

namespace SageExchangeDesktopSamples.Samples
{
    partial class Transactions
    {
        static public void Sale()
        {
            Console.WriteLine("Press any key to run Sale.");
            Console.ReadLine();
            Console.WriteLine("Running Sale transaction.");
            Console.WriteLine();
            // -----------------------------------------------

            // This sample demonstrates a Sale request through Sage Exchange Desktop.
            // A Sale is the most basic type of transaction.

            // See the Utilities class for more on building requests.
            Request_v1 SedRequest = RequestFactory.GetPaymentRequest();

            // Setting the transaction type to "11" indicates that we want to run a Sale.
            SedRequest.Payments[0].TransactionBase.TransactionType = "11";

            // Convert the request object to an XML string.
            string XmlRequest = SedRequest.ToXml();

            /* 
             * Note that, in many cases, it makes more sense to build the XML string directly.
             * 
             * For example, if you know your requests are all going to be simple sales,
             * maybe just varying by amount and order number, you could manipulate an XML
             * string literal with String.Format() -- this would probably be easier than
             * piecing together a request object and calling its ToXml() method.
             * 
             * See the "XML Messaging" document for more on SED requests.
             * 
             */

            // The SED API exposes a client object that manages messaging 
            // between your application and Sage Exchange Desktop.
            var ApiClient = new ModuleClient();

            // This is the point at which the UI pops up to collect the payment information.
            IModuleResponse ApiResponse = ApiClient.GetResponse(XmlRequest);

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

            Console.WriteLine();
        }
    }
}
