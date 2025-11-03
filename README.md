# Tenor-SDK-v2

기존 Tenor에서 제공하는 [SDK](https://github.com/Tenor-Inc/tenor-unity-ar-sdk)가 v1 버전만 지원하고 있어 v2 전용 버전으로 수정한 패키지입니다.

# 사용 방법

**예시 프로젝트를 실행하려면 반드시 [3DI70R의 Unity-GifDecoder](https://github.com/3DI70R/Unity-GifDecoder?tab=readme-ov-file)가 필요합니다!**

- 올바른 API 키로 SDK를 초기화합니다.
- 아래 파라미터에 존재하는 올바른 요청 클래스를 생성합니다.
- API 엔드포인트에서 정보를 가져오는 메서드를 호출합니다.

```csharp
// 트랜드 검색 예시
public void ExampleTrendingTenorGIF() {

    // SDK 초기화
    TenorAPI.Initialize ("LIVDSRZULELA");
    
    // 요청 데이터 세팅
    TrendingRequest request = new TrendingRequest ();
    request.pos = "";
    request.limit = 5;
    
    // 코루틴으로 비동기 호출
    StartCoroutine(TenorAPI.Trending(request, ProcessAnswers));
    
}

void ProcessAnswers(Response data) {
    // 요청 성공 시 엑션
}
```

# 메서드

`Tenor-SDK-v2`는 아래의 메서드를 제공하고 있습니다.

```csharp
  public static IEnumerator Search(SearchRequest request, DelegateResponseAnswer delegateSearch);
  public static IEnumerator Featured(FeaturedRequest request, DelegateResponseAnswer delegateFeatured);
  public static IEnumerator Categories(CategoriesRequest request, DelegateTagCollectionAnswer delegateCategories);
  public static IEnumerator SearchSuggestions(SearchSuggestionsRequest request, DelegateStringAnswer delegateSearchSuggestions);
  public static IEnumerator AutoComplete(AutoCompleteRequest request, DelegateStringAnswer delegateAutoComplete);
  public static IEnumerator TrendingSearchTerms(TrendingTermsRequest request, DelegateStringAnswer delegateTrendingTerms);
  public static IEnumerator RegisterShare(RegisterShareRequest request, DelegateStringAnswer delegateRegisterShare);
```


# 예시 프로젝트

`Tenor-SDK-v2`는 아래의 예시 프로젝트를 제공하고 있습니다.

- SearchExample
- SearchSuggestionExample
- FeaturedExample
- TrendingTermsExample
- CategoriesExample
