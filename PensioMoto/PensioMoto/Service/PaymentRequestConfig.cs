using System;

namespace PensioMoto.Service
{
	public class PaymentRequestConfig
	{
		//config[callback_form]	Sending this will override the "callback form" setting on the terminal	String (Url)
		//config[callback_ok]	Sending this will override the "callback ok" setting on the terminal	String (Url)
		//config[callback_fail]	Sending this will override the "callback fail" setting on the terminal	String (Url)
		//config[callback_redirect]	Sending this will override the "callback redirect" setting on the terminal	String (Url)
		//config[callback_open]	Sending this will override the "callback open" setting on the terminal	String (Url)
		//config[callback_notification]	Sending this will override the "callback notification" setting on the terminal	String (Url)
		/*config[callback_verify_order] By settings this, a check will be made at the last possible time before taking the payment. This is used to verify that stock, discounts, etc. are still valid for the order/shopping basket. This callback will be done in the same way as other callbacks, but you can prepend GET parameters to the URL if you need anything in particular which is not part of the normal POST parameters. 

To allow the payment you must return an HTML/TEXT response with the value "OKAY". Anything else will be assumed to be a sign that we should abort/decline the payment and will be placed as the error message. An example could be "Some of the items in the basked are out of stock". 

If your server responds with any other http response code than 200, the payment will fail with an error. 

To ensure consistance we will strip HTML/XML tags, and we will only allow the first 255 characters to become the error message when the callback returns something different than "OKAY"
		*/
		
		//fraud_service
	}
}

