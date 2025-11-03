using UnityEngine;
using UnityEngine.UI;


public class GalleryCategories : MonoBehaviour {

	public Categories[] data;
	public GameObject container;
	public GameObject CategoryPrefab;

	// Use this for initialization
	void Start () {		
	}

	// Update is called once per frame
	void Update () {

	}

	public void LoadAssets() {

		// Remove all child from previous display
		foreach (Transform child in container.transform) {
			Destroy(child.gameObject);
		}	

		// Create all elements
		for (int i = 0; i < data.Length; i++) {

			Categories categories = data[i];

			// Instatiate New Game Object
			GameObject tenorGO = Instantiate (CategoryPrefab, container.transform);

			GifPlayer gifPlayer = tenorGO.GetComponent<GifPlayer>();
			StartCoroutine(gifPlayer.PlayGif(categories.image));

			Text t = tenorGO.GetComponentInChildren<Text>();
			t.text = data[i].name;

		}
	}
}
