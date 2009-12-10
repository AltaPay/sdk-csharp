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
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MyApplicationContext());
        }
    }

	public class MyApplicationContext : ApplicationContext
	{
		public MyApplicationContext()
		{
			IMotoDialog dialog = new MotoDialog(new MotoForm(), new MerchantApi());
			dialog.Initialize("http://gateway.testserver.pensio.com/merchant.php/API/", "shop api", "testpassword", "Pensio Test Terminal", Guid.NewGuid().ToString(), 42.42, 208, PaymentType.payment);

			dialog.SetCreditCard("411100******0000", "156d557b225920dc3a231f777e44c975c1e6fe70");

			PaymentResult result = dialog.Show();

			MessageBox.Show(result.Result.ToString());
		}
	}
}
