//
// Tenor Unity SDK - Unity libraries for Tenor GIF
// =================================================
//
// Copyright (C) 2017 by Dift.co (http://dift.co)
// https://www.tenor.com
//
// ***********************************************************************************************************************
//
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file except in compliance with
// the License. You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software distributed under the License is distributed on
// an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the License for the
// specific language governing permissions and limitations under the License.
//
// ***********************************************************************************************************************


namespace TenorSDK.Request
{	
	public class FeatureRequest : RequestGET
	{
		public string key; 	// client key for privileged API access
		public string type; // 반환되는 카테고리 유형을 결정합니다. 기본값은 featured입니다. 허용되는 값은 featured 및 trending입니다.
		public string searchfilter;
		public string country;
		public string media_filter;
		public string limit;
		public string pos;

		private string Uri = "/trending";

		public FeatureRequest() {
		}

		public string getQueryString(string key) {
			return Uri + "?key=" + key + generateQueryString ();
		}
			
	}
}