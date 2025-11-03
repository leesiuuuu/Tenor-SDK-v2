using UnityEngine;
using UnityEngine.UI;

using TenorSDK;
using TenorSDK.Request;

public class SearchExample : MonoBehaviour {

	public GalleryGIFs resultGIFs; 
	public InputField inputTag;

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {

	}

	// Search GIFs
	public void SearchTenorGIF() {

		// Initialize SDK
		TenorAPI.Initialize ("TEST_API_KEY");

		// Prepare Request data
		SearchRequest request = new SearchRequest ();
		request.q = inputTag.text;
		request.limit = "15";

		// Call Coroutine to not freeze
		StartCoroutine(TenorAPI.Search(request, ProcessAnswers));

	}

	void ProcessAnswers(Response data) {
		resultGIFs.data = data.results;
		resultGIFs.LoadAssets ();
	}

}
