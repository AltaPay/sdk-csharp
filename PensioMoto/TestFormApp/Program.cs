using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using PensioMoto;
using PensioMoto.Service;

namespace TestFormApp
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
			Moto moto = new Moto();
			moto.Initialize("http://10.101.97.14/merchant.php/API/", "shop api", "testpassword", "Pensio Test Terminal", Guid.NewGuid().ToString(), 42.42, 208, "payment");

			Merchant api = new Merchant();
			api.Initialize("http://10.101.97.14/merchant.php/API/", "shop api", "testpassword", "Pensio Test Terminal");

			//api.CaptureWithIdentifier("73", 10, "whatisthis");
			//api.RefundWithIdentifier("73", 10, "whatisthis2");

			IComFundingsResult result = api.GetFundings(1);
			MessageBox.Show(result.getFunding(0).getLine(0).ExchangeRate);
			
			int a = 0;
			a++;
			/*
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MyApplicationContext());
			 * */
        }
    }

	public class MyApplicationContext : ApplicationContext
	{
		public MyApplicationContext()
		{
			
			/*
			moto.AddCreditCard("411100******0000", "156d557b225920dc3a231f777e44c975c1e6fe70");
			moto.SetAvsInfo("my first name", "my last name", "Albertslund", "my postal code", "my city", "my region", "my country", "my phone", "my email");

			IComPaymentResult result = moto.Show();

			api.GetFundings(0);

			MessageBox.Show(result.Result.ToString() + " Merchant message:"  + result.ResultMessage +  " avs message:" + result.Payment.AddressVerification + " " + result.Payment.AddressVerificationDescription);
			*/
			//MessageBox.Show(api.Split(result.Payment.PaymentId, 10.66).Payment.PaymentId);
		}
	}
}
