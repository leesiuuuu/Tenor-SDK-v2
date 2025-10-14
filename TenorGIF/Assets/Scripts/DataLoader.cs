using System.Collections;
using System.Threading;
using TenorSDK;
using TenorSDK.Request;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DataLoader : MonoBehaviour
{
	[SerializeField]
	private string limitPerPage = "50";
	[SerializeField]
	private int totalToLoad = 1000;
	[SerializeField]
	private Image loadingBar;

	private int loadedCount = 0;

	private string pos = "";
	private bool isLoading = false;

	public UnityEvent OnLoadComplete = new UnityEvent();

	private void Start()
	{
		StartLoading();
	}

	public void StartLoading()
	{
		loadingBar.fillAmount = 0f;	
		StartCoroutine(LoadAllGIFs());
	}

	public IEnumerator LoadAllGIFs()
	{
		Init();
		if (isLoading) yield break;
		isLoading = true;

		GameManager.instance.results.Clear();

		while(loadedCount < totalToLoad)
		{
			float value = (float)loadedCount / totalToLoad;
			SearchRequest request = new SearchRequest();
			request.q = GameManager.instance.Subject;
			request.limit = limitPerPage;
			if(!string.IsNullOrEmpty(pos))
				request.pos = pos;

			yield return TenorAPI.Search(request, OnPrograss);

			loadingBar.fillAmount = value;
			
		}
		yield return new WaitForSeconds(0.2f);

		isLoading = false;
		Debug.Log($"총 {loadedCount}개 로딩 완료!");
		OnLoadComplete?.Invoke();
	}

	private void OnPrograss(Response data)
	{
		GameManager.instance.results.AddRange(data.results);
		loadedCount += data.results.Length;
		Debug.Log($"누적 로드 개수 : {loadedCount}");

		pos = data.next;
	}

	private void Init()
	{
		TenorAPI.Initialize("AIzaSyAWDcOdghingSu3gXlbv26sie7AZLlY1-Q");
		loadedCount = 0;
		isLoading = false;
		pos = "";
	}
}
