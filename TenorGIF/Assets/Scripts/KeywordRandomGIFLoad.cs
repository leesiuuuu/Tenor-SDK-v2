using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TenorSDK;
using TenorSDK.Request;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.Video;

public class KeywordRandomGIFLoad : MonoBehaviour
{
	private GalleryGIFs gifs;
	private GameObject container;

	[SerializeField]
	private float elementWidth;

	[SerializeField]
	private float elementHeight;

	[SerializeField]
	private string nameTag;

	[SerializeField]
	private bool isRandom = false;

	public Result ResultObject
	{
		get { return r; }
	}

	[SerializeField]
	private Result r;

	private void Start()
	{
		if(!isRandom)
			SearchTenorGIF();
	}
	public void SearchTenorGIF()
	{
		// Initialize SDK
		TenorAPI.Initialize("AIzaSyAWDcOdghingSu3gXlbv26sie7AZLlY1-Q");

		// Prepare Request data
		SearchRequest request = new SearchRequest();
		request.q = nameTag;
		request.limit = "1";

		// Call Coroutine to not freeze
		StartCoroutine(TenorAPI.Search(request, ProcessAnswers));
	}
	public void RandomSearchTenorGIF()
	{
		int max = GameManager.instance.results.Count;
		int idx = Random.Range(0, max);
		Result randResult = GameManager.instance.results[idx];
		r = randResult;
		LoadAssets(randResult);
	}

	void ProcessAnswers(Response data)
	{
		r = data.results[0];
		LoadAssets(data.results[0]);
	}

	public void LoadAssets(Result result)
	{
		var media = result.media_formats.mp4;
		if (media == null)
		{
			Debug.LogWarning($"No MP4 media found for {result.title}");
			return;
		}

		GameObject videoGO = gameObject;

		// 🔹 VideoPlayer 구성
		VideoPlayer vp = videoGO.GetComponent<VideoPlayer>();
		RawImage rawImage = videoGO.GetComponentInChildren<RawImage>();
		vp.source = VideoSource.Url;
		vp.url = media.url;
		vp.playOnAwake = false;
		vp.isLooping = true;
		vp.renderMode = VideoRenderMode.APIOnly;
		StartCoroutine(PrepareAndPlay(vp, rawImage));
	}

	private IEnumerator PrepareAndPlay(VideoPlayer vp, RawImage rawImage)
	{
		vp.Prepare();

		while (!vp.isPrepared)
			yield return null;

		// 🔹 원본 비율 가져오기
		float videoWidth = vp.texture.width;
		float videoHeight = vp.texture.height;

		if(videoWidth > videoHeight)
		{
			float ratio = elementHeight / videoHeight;
			float adjustedWidth = videoWidth * ratio;

			RectTransform rt = rawImage.GetComponent<RectTransform>();
			rt.sizeDelta = new Vector2(adjustedWidth, elementHeight);
		}
		else
		{
			float ratio = elementWidth / videoWidth;
			float adjustedHeight = videoHeight * ratio;

			RectTransform rt = rawImage.GetComponent<RectTransform>();
			rt.sizeDelta = new Vector2(elementWidth, adjustedHeight);
		}


		rawImage.texture = vp.texture;
		vp.Play();
	}
}
