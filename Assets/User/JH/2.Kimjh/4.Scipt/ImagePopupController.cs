using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ImagePopupController : MonoBehaviour
{
    [Header("팝업 설정")]
    [SerializeField] private GameObject popupPanel;          // 팝업 패널
    [SerializeField] private Image popupImage;               // 팝업될 이미지
    [SerializeField] private Button closeButton;             // 닫기 버튼
    [SerializeField] private float delayTime = 10f;          // 팝업 지연 시간 (초)
    [SerializeField] private float fadeInDuration = 0.5f;    // 페이드인 시간
    [SerializeField] private float fadeOutDuration = 0.3f;   // 페이드아웃 시간

    [Header("팝업 옵션")]
    [SerializeField] private bool autoClose = false;         // 자동 닫기 여부
    [SerializeField] private float autoCloseDelay = 5f;      // 자동 닫기 시간
    [SerializeField] private bool showOnlyOnce = true;       // 한 번만 표시 여부

    private CanvasGroup canvasGroup;
    private bool hasShown = false;

    void Start()
    {
        // 초기 설정
        InitializePopup();

        // 10초 후 팝업 표시
        StartCoroutine(ShowPopupAfterDelay());
    }

    void InitializePopup()
    {
        // CanvasGroup 컴포넌트 확인 및 추가
        if (popupPanel != null)
        {
            canvasGroup = popupPanel.GetComponent<CanvasGroup>();
            if (canvasGroup == null)
            {
                canvasGroup = popupPanel.AddComponent<CanvasGroup>();
            }
        }

        // 팝업 패널 초기 상태 설정 (비활성화)
        if (popupPanel != null)
        {
            popupPanel.SetActive(false);
            canvasGroup.alpha = 0f;
        }

        // 닫기 버튼 이벤트 연결
        if (closeButton != null)
        {
            closeButton.onClick.AddListener(ClosePopup);
        }
    }

    IEnumerator ShowPopupAfterDelay()
    {
        // 한 번만 표시 옵션이 켜져있고 이미 표시했다면 중단
        if (showOnlyOnce && hasShown)
            yield break;

        // 지정된 시간만큼 대기
        yield return new WaitForSeconds(delayTime);

        // 팝업 표시
        ShowPopup();

        // 자동 닫기가 활성화되어 있다면
        if (autoClose)
        {
            yield return new WaitForSeconds(autoCloseDelay);
            ClosePopup();
        }
    }

    public void ShowPopup()
    {
        if (popupPanel == null) return;

        // 팝업 패널 활성화
        popupPanel.SetActive(true);
        hasShown = true;

        // 페이드인 애니메이션
        StartCoroutine(FadeIn());

        Debug.Log("팝업이 표시되었습니다!");
    }

    public void ClosePopup()
    {
        if (popupPanel == null) return;

        // 페이드아웃 애니메이션 후 비활성화
        StartCoroutine(FadeOutAndHide());

        Debug.Log("팝업이 닫혔습니다!");
    }

    IEnumerator FadeIn()
    {
        float elapsedTime = 0f;

        while (elapsedTime < fadeInDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeInDuration);
            canvasGroup.alpha = alpha;
            yield return null;
        }

        canvasGroup.alpha = 1f;
    }

    IEnumerator FadeOutAndHide()
    {
        float elapsedTime = 0f;
        float startAlpha = canvasGroup.alpha;

        while (elapsedTime < fadeOutDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, 0f, elapsedTime / fadeOutDuration);
            canvasGroup.alpha = alpha;
            yield return null;
        }

        canvasGroup.alpha = 0f;
        popupPanel.SetActive(false);
    }

    // 외부에서 팝업을 즉시 표시하고 싶을 때 사용
    public void ShowPopupImmediately()
    {
        StopAllCoroutines();
        ShowPopup();
    }

    // 외부에서 타이머를 리셋하고 싶을 때 사용
    public void ResetTimer()
    {
        StopAllCoroutines();
        hasShown = false;
        StartCoroutine(ShowPopupAfterDelay());
    }

    void OnDestroy()
    {
        // 메모리 누수 방지를 위해 이벤트 리스너 제거
        if (closeButton != null)
        {
            closeButton.onClick.RemoveListener(ClosePopup);
        }
    }
}

// 사용법 주석:
/*
1. 빈 GameObject를 생성하고 이 스크립트를 붙입니다.
2. UI Canvas 안에 팝업용 Panel을 생성합니다.
3. Panel 안에 Image와 Button(닫기용)을 배치합니다.
4. Inspector에서 다음을 연결합니다:
   - Popup Panel: 팝업 패널 GameObject
   - Popup Image: 표시할 이미지 컴포넌트
   - Close Button: 닫기 버튼 컴포넌트
5. Delay Time을 10으로 설정 (기본값)
6. 필요에 따라 다른 옵션들도 조정합니다.

선택사항:
- 팝업 패널에 CanvasGroup 컴포넌트를 미리 추가하면 더 부드러운 애니메이션이 가능합니다.
- 배경을 어둡게 하려면 Panel 뒤에 어두운 Image를 추가하세요.
*/


