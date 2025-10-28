using System;
using System.Collections;
using System.Collections.Generic;
using ThreeDISevenZeroR.UnityGifDecoder;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class GifPlayer : MonoBehaviour
{
	[SerializeField]
	private RawImage rawImage;

	public bool Stop = false;

	public IEnumerator PlayGif(string uri)
	{
		UnityWebRequest www = UnityWebRequest.Get(uri);
		yield return www.SendWebRequest();

		if (www.result != UnityWebRequest.Result.Success)
		{
			Debug.LogError("Failed Download GIF: " + www.error);
			yield break;
		}

		byte[] gifBytes = www.downloadHandler.data;

		List<Texture2D> textures = new List<Texture2D>();
		List<float> delays = new List<float>();

		int gifWidth = 0;
		int gifHeight = 0;

		using (GifStream gifStream = new GifStream(gifBytes))
		{
			while (gifStream.HasMoreData)
			{
				if (gifStream.CurrentToken == GifStream.Token.Image)
				{
					var image = gifStream.ReadImage();
					var tex = new Texture2D(gifStream.Header.width, gifStream.Header.height, TextureFormat.ARGB32, false);
					tex.SetPixels32(image.colors);
					tex.Apply();

					textures.Add(tex);
					delays.Add(image.SafeDelaySeconds);
				}
				else
				{
					gifStream.SkipToken();
				}
			}
		}

		gifWidth = textures[0].width;
		gifHeight = textures[0].height;

		RectTransform rt = rawImage.rectTransform;
		float aspect = (float)gifWidth / gifHeight;

		RectTransform parent = rt.parent as RectTransform;
		Vector2 parentSize = parent.rect.size;

		float parentAspect = parentSize.x / parentSize.y;

		if (aspect > parentAspect)
		{
			float targetHeight = parentSize.y;
			float targetWidth = targetHeight * aspect;
			rt.sizeDelta = new Vector2(targetWidth, targetHeight);
		}
		else
		{
			float targetWidth = parentSize.x;
			float targetHeight = targetWidth / aspect;
			rt.sizeDelta = new Vector2(targetWidth, targetHeight);
		}

		rt.anchoredPosition = Vector2.zero;
		rt.anchorMin = new Vector2(0.5f, 0.5f);
		rt.anchorMax = new Vector2(0.5f, 0.5f);
		rt.pivot = new Vector2(0.5f, 0.5f);

		while (!Stop)
		{
			for (int i = 0; i < textures.Count; i++)
			{
				if (Stop) yield break;
				rawImage.texture = textures[i];
				yield return new WaitForSeconds(delays[i]);
			}
		}
	}
}
