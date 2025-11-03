using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TenorSDK;
using TenorSDK.Request;

public class TrendingTermsExample : MonoBehaviour {

	public GalleryStrings resultStrings; 

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {

	}
	
	// Search Trending Gifs
	public void TrendingTermsTenorGIF() {

		// Initialize SDK
		TenorAPI.Initialize ("AIzaSyAWDcOdghingSu3gXlbv26sie7AZLlY1-Q");

		// Prepare Request data
		TrendingTermsRequest request = new TrendingTermsRequest ();
		request.limit = "10";

		// Call Coroutine to not freeze
		StartCoroutine(TenorAPI.TrendingSearchTerms(request, ProcessAnswers));

	}

	void ProcessAnswers(ResultStringCollection data) {
		resultStrings.data = data.results;
		resultStrings.showTags ();
	}

}
