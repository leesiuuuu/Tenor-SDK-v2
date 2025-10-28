using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using TenorSDK;
using TenorSDK.Request;

public class GalleryGIFs : MonoBehaviour
{
    public Result[] data;
    public GameObject container;
    public GameObject gifPrefab;
    public float elementWidth = 400.0f;

    public void LoadAssets()
    {
        foreach (Transform child in container.transform)
            Destroy(child.gameObject);

        foreach (var result in data)
        {
            var media = result.media_formats.nanogif ?? result.media_formats.tinygif ?? result.media_formats.gif;
            if (media == null)
            {
                Debug.LogWarning($"No GIF media found for {result.title}");
                continue;
            }

            GameObject videoGO = Instantiate(gifPrefab, container.transform);

            float ratio = elementWidth / media.dims[0];
            float elementHeight = media.dims[1] * ratio;

            RectTransform rt = videoGO.GetComponent<RectTransform>();
            rt.sizeDelta = new Vector2(elementWidth, elementHeight);
            
            GifPlayer gifPlayer = videoGO.GetComponent<GifPlayer>();
            StartCoroutine(gifPlayer.PlayGif(media.url));
        }
    }
}
