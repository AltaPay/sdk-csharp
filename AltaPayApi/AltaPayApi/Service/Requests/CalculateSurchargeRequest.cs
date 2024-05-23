namespace AltaPay.Service
{
    public class CalculateSurchargeRequest : BasePaymentRequestRequest
	{
		/// <summary>
		/// Either 3 letter or 3 digit currency code. ISO-4217
		/// </summary>
		public string Currency { get; set; }
		/// <summary>
		/// The id of an existing payment or subscription to base the calculation on.
		/// </summary>
		public string PaymentId { get; set; }
	}
}
