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
    public GameObject gifPrefab; // prefab: RawImage + VideoPlayer
    public float paddingX = 20.0f;
    public float paddingY = 20.0f;
    public float elementWidth = 400.0f;
    public int columns = 2;

    public void LoadAssets()
    {
        // 이전 자식 제거
        foreach (Transform child in container.transform)
            Destroy(child.gameObject);

        float[] columnHeights = new float[columns]; // 각 열의 누적 높이

        foreach (var result in data)
        {
            // 🔹 MP4 링크 가져오기
            var media = result.media_formats.nanomp4 ?? result.media_formats.tinymp4 ?? result.media_formats.mp4;
            if (media == null)
            {
                Debug.LogWarning($"No MP4 media found for {result.title}");
                continue;
            }

            // 🔹 프리팹 생성
            GameObject videoGO = Instantiate(gifPrefab, container.transform);

            // 🔹 비율 계산
            float ratio = elementWidth / media.dims[0];
            float elementHeight = media.dims[1] * ratio;

            RectTransform rt = videoGO.GetComponent<RectTransform>();
            rt.sizeDelta = new Vector2(elementWidth, elementHeight);

            // 🔹 VideoPlayer 구성
            VideoPlayer vp = videoGO.GetComponent<VideoPlayer>();
            RawImage rawImage = videoGO.GetComponentInChildren<RawImage>();
            vp.source = VideoSource.Url;
            vp.url = media.url;
            vp.playOnAwake = false;
            vp.isLooping = true;
            vp.renderMode = VideoRenderMode.APIOnly;
            StartCoroutine(PrepareAndPlay(vp, rawImage));

            // 🔹 가장 낮은 열에 배치
            int targetColumn = 0;
            float minHeight = columnHeights[0];
            for (int i = 1; i < columns; i++)
            {
                if (columnHeights[i] < minHeight)
                {
                    targetColumn = i;
                    minHeight = columnHeights[i];
                }
            }

            float posX = targetColumn * (elementWidth + paddingX);
            float posY = -columnHeights[targetColumn];

            videoGO.transform.localPosition = new Vector3(posX, posY, 0);

            // 🔹 열 높이 갱신
            columnHeights[targetColumn] += elementHeight + paddingY;
        }

        // ✅ 컨테이너 높이 조정 (가장 높은 열 기준)
        float maxHeight = 0;
        foreach (float h in columnHeights)
            if (h > maxHeight) maxHeight = h;

        RectTransform containerRT = container.GetComponent<RectTransform>();
        containerRT.sizeDelta = new Vector2(containerRT.sizeDelta.x, maxHeight);
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
