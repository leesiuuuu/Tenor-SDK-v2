using TenorSDK.Request;

namespace TenorSDK.Request
{
	public class TrendingTermsRequest : RequestGET
	{
		public string key;
		public string client_key;
		public string country;
		public string locale;
		public string limit;

		private string Uri = "/trending_terms?";

		public TrendingTermsRequest()
		{
		}

		public string getQueryString(string key)
		{
			return Uri + "key=" + key + generateQueryString();
		}
	}
}