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
			Debug.LogError("GIF 다운로드 실패: " + www.error);
			yield break;
		}

		byte[] gifBytes = www.downloadHandler.data;

		List<Texture2D> textures = new List<Texture2D>();
		List<float> delays = new List<float>();

		// 1. 모든 프레임 읽기
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

		// 2. 프레임 리스트 반복 재생
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
