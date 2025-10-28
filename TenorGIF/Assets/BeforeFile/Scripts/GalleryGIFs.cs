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
            var media = result.media_formats.nanomp4 ?? result.media_formats.tinymp4 ?? result.media_formats.mp4;
            if (media == null)
            {
                Debug.LogWarning($"No MP4 media found for {result.title}");
                continue;
            }

            GameObject videoGO = Instantiate(gifPrefab, container.transform);

            float ratio = elementWidth / media.dims[0];
            float elementHeight = media.dims[1] * ratio;

            RectTransform rt = videoGO.GetComponent<RectTransform>();
            rt.sizeDelta = new Vector2(elementWidth, elementHeight);

            VideoPlayer vp = videoGO.GetComponent<VideoPlayer>();
            RawImage rawImage = videoGO.GetComponentInChildren<RawImage>();
            vp.source = VideoSource.Url;
            vp.url = media.url;
            vp.playOnAwake = false;
            vp.isLooping = true;
            vp.audioOutputMode = VideoAudioOutputMode.None;
            vp.renderMode = VideoRenderMode.APIOnly;
            StartCoroutine(PrepareAndPlay(vp, rawImage));
        }
    }

    private IEnumerator PrepareAndPlay(VideoPlayer vp, RawImage rawImage)
    {
        vp.Prepare();

        while (!vp.isPrepared)
            yield return null;

        rawImage.texture = vp.texture;
        vp.Play();
    }
}
