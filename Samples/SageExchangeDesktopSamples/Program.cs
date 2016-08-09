using System;

namespace SageExchangeDesktopSamples
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Press any key to begin samples.");
            Console.ReadLine();
            // -----------------------------------------------

            // The Config class shares data between requests. Let's get it set up now:
            Config.merchantId = "999999999997";
            Config.merchantKey = "K3QD6YWyhfD";
            Config.applicationId = "DEMO";
            Config.languageId = "EN";
            Config.paymentAmount = 1.00;

            // The Samples class contains various methods to demonstrate various common scenarios.
            // Each sample method is stored in a file of the same name; eg, Sale() is in Sale.cs.

            // Running a simple Sale transaction is a good way to get started:
            Samples.Transactions.Sale();
            //Samples.Transactions.AuthAndCapture();

            // -----------------------------------------------
            Console.WriteLine("Press any key to quit.");
            Console.ReadLine(); 
        }
    }
}
