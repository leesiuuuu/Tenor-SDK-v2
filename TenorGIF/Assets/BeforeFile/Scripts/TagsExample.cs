using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TenorSDK;
using TenorSDK.Request;

public class CategoriesExample : MonoBehaviour {

	public GalleryTags resultTags; 

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {

	}

	// Search Tags
	public void TagsTenorGIF() {

		// Initialize SDK
		TenorAPI.Initialize ("AIzaSyAWDcOdghingSu3gXlbv26sie7AZLlY1-Q");

		// Prepare Request data
		CategoriesRequest request = new CategoriesRequest();
		request.type = "featured";

		// Call Coroutine to not freeze
		StartCoroutine(TenorAPI.Categories(request, ProcessAnswers));
	}

	void ProcessAnswers(ResultCategories data) {
		resultTags.data = data.categories;
		resultTags.LoadAssets ();
	}

}
