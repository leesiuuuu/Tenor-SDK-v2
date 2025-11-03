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


using System;
using System.Threading;
using System.Linq;
using System.ComponentModel;
using System.Collections;

using UnityEngine;
using TenorSDK.Request;
using UnityEngine.Networking;

namespace TenorSDK
{	
	public static class TenorAPI
	{

		//
		// NOTE: This example include a restricted, rate limited example key (LIVDSRZULELA) for you to use while evaluating our API. 
		// Before deploying your integration to production, please request your own API key (it's free).
		//

		public static string key = "TEST_API_KEY"; 	// client key for privileged API access
		private static string TenorAPIUri = "https://tenor.googleapis.com/v2";

		public delegate void DelegateResponseAnswer(Response inputObject);
		public delegate void DelegateStringAnswer(ResultStringCollection inputObject);
		public delegate void DelegateTagCollectionAnswer(ResultCategories inputObject);

		public static void Initialize(string customKey)
		{
			key = customKey;
		}

		// Method to call API End Point: Search
		public static IEnumerator Search(SearchRequest request, DelegateResponseAnswer delegateSearch)
		{
			return _apiCallResponse (request.getQueryString (key), delegateSearch);
		}

		// Method to call API End Point: Featured
		public static IEnumerator Featured(FeaturedRequest request, DelegateResponseAnswer delegateFeatured)
		{
			return _apiCallResponse (request.getQueryString (key), delegateFeatured);
		}

		// Method to call API End Point: Categories
		public static IEnumerator Categories(CategoriesRequest request, DelegateTagCollectionAnswer delegateCategories)
		{
			return _apiCallTagCollection (request.getQueryString (key), delegateCategories);
		}

		// Method to call API End Point: Search Suggestions 
		public static IEnumerator SearchSuggestions(SearchSuggestionsRequest request, DelegateStringAnswer delegateSearchSuggestions)
		{
			return _apiCallStringCollection (request.getQueryString (key), delegateSearchSuggestions);
		}

		// Method to call API End Point: Auto Complete
		public static IEnumerator AutoComplete(AutoCompleteRequest request, DelegateStringAnswer delegateAutoComplete)
		{
			return _apiCallStringCollection (request.getQueryString (key), delegateAutoComplete);
		}

		// Method to call API End Point: Trending Search Terms
		public static IEnumerator TrendingSearchTerms(TrendingTermsRequest request, DelegateStringAnswer delegateTrendingTerms)
		{
			return _apiCallStringCollection(request.getQueryString(key), delegateTrendingTerms);
		}

		// Method to call API End Point: Register Share
		public static IEnumerator RegisterShare(RegisterShareRequest request, DelegateStringAnswer delegateRegisterShare)
		{
			return _apiCallStringCollection (request.getQueryString (key), delegateRegisterShare);
		}



		/* 
		 * Internal API Calls 
		 * 
		 */ 

		private static IEnumerator _apiCallResponse(string uri, DelegateResponseAnswer delegateSearch) {
			UnityWebRequest www = UnityWebRequest.Get(TenorAPIUri + uri);
			yield return www.SendWebRequest();
			if (www.error == "" || www.error == null) {
				Response data = JsonUtility.FromJson<Response>(www.downloadHandler.text);
				if (delegateSearch != null) {
					
					delegateSearch (data);
				}
			} else {
				throw new Exception(www.error);
			} 			
		}

		private static IEnumerator _apiCallTagCollection(string uri, DelegateTagCollectionAnswer delegateSearch) {
			UnityWebRequest www = UnityWebRequest.Get(TenorAPIUri + uri);
			yield return www.SendWebRequest();
			if (www.error == "" || www.error == null) {
				ResultCategories data = JsonUtility.FromJson<ResultCategories>(www.downloadHandler.text);
				if (delegateSearch != null) {
					delegateSearch (data);
				}
			} else {
				throw new Exception(www.error);
			} 			
		}

		private static IEnumerator _apiCallStringCollection(string uri, DelegateStringAnswer delegateSearch) {
			UnityWebRequest www = UnityWebRequest.Get(TenorAPIUri + uri);
			yield return www.SendWebRequest();

			if (www.error == "" || www.error == null) {
				ResultStringCollection data = JsonUtility.FromJson<ResultStringCollection>(www.downloadHandler.text);
				if (delegateSearch != null) {
					delegateSearch (data);
				}
			} else {
				throw new Exception(www.error);
			} 			
		}
	}
}