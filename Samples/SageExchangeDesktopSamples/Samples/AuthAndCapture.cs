using clSedApi;
using SpsXmlCL.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SageExchangeDesktopSamples.Samples
{
    partial class Transactions
    {
        static public void AuthAndCapture()
        {
            Auth();
            Capture();
        }
        static public void Auth()
        {
            Console.WriteLine("Press any key to run an Authorization.");
            Console.ReadLine();
            Console.WriteLine("Running Authorization transaction.");
            Console.WriteLine();
            // -----------------------------------------------

            // An Authorization charge is like a Sale, except it needs to be finalized
            // before settlement. You can use this as an opportunity to adjust the amount,
            // which makes it a great option for adding tips, shipping, etc.

            // See the Utilities class for more on building requests.
            Request_v1 SedRequest = RequestFactory.GetPaymentRequest();

            // Setting the transaction type to "12" indicates that we want to run an Authorization.
            SedRequest.Payments[0].TransactionBase.TransactionType = "12";

            // Convert the request object to an XML string.
            string XmlRequest = SedRequest.ToXml();

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

                // Printing out the transaction amount, so we can compare after a capture.
                Console.WriteLine("Amount: " + SedResponse.PaymentResponses[0].TransactionResponse.Amount.ToString());

                // We'll save the transaction reference for later. It's used for voids, refunds, etc.
                Config.vanReference = SedResponse.PaymentResponses[0].TransactionResponse.VANReference;
            }

            Console.WriteLine();
        }
        static public void Capture()
        {
            Console.WriteLine("Press any key to run an Capture.");
            Console.ReadLine();
            Console.WriteLine("Running Capture transaction.");
            Console.WriteLine();
            // -----------------------------------------------

            // An Capture transaction finalizes an Authorization, indicating that it is
            // ready for settlement. During Capture, we can update the transaction amount.

            if (String.IsNullOrWhiteSpace(Config.vanReference))
            {
                Console.WriteLine("A capture request requires a previous Authorization.");
                Console.WriteLine("The AuthAndCapture() method wraps the Auth and Cap in a single method.");
            }
            else
            {
                // See the Utilities class for more on building requests.
                Request_v1 SedRequest = RequestFactory.GetPaymentRequest();

                // Setting the transaction type to "03" indicates that we want to run a Capture.
                // To require user interaction, use "13" instead.
                SedRequest.Payments[0].TransactionBase.TransactionType = "03";

                // Reference the Authorization that we want to Capture.
                SedRequest.Payments[0].TransactionBase.VANReference = Config.vanReference;
                // Add 10 cents to our original dollar.
                SedRequest.Payments[0].TransactionBase.Amount = 1.10;

                // Convert the request object to an XML string.
                string XmlRequest = SedRequest.ToXml();

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

                    // Printing out the transaction amount, so we can compare to the original Authorization.
                    Console.WriteLine("Amount: " + SedResponse.PaymentResponses[0].TransactionResponse.Amount.ToString());

                    // We'll save the transaction reference for later. It's used for voids, refunds, etc.
                    Config.vanReference = SedResponse.PaymentResponses[0].TransactionResponse.VANReference;
                }

                Console.WriteLine();
            }

        }
    }
}
