using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TenorSDK;
using TenorSDK.Request;

public class FeaturedExample : MonoBehaviour {

	public GalleryGIFs resultGIFs; 

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// Search Trending GIFs
	public void TrendingTenorGIF() {

		// Initialize SDK
		TenorAPI.Initialize ("AIzaSyAWDcOdghingSu3gXlbv26sie7AZLlY1-Q");

		// Prepare Request data
		FeatureRequest request = new FeatureRequest();
		request.limit = "10";

		// Call Coroutine to not freeze
		StartCoroutine(TenorAPI.Featured(request, ProcessAnswers));
	}

	void ProcessAnswers(Response data) {
		resultGIFs.data = data.results;
		resultGIFs.LoadAssets ();
	}
				
}
