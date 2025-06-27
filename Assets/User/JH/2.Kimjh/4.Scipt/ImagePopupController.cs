using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ImagePopupController : MonoBehaviour
{
    [Header("�˾� ����")]
    [SerializeField] private GameObject popupPanel;          // �˾� �г�
    [SerializeField] private Image popupImage;               // �˾��� �̹���
    [SerializeField] private Button closeButton;             // �ݱ� ��ư
    [SerializeField] private float delayTime = 10f;          // �˾� ���� �ð� (��)
    [SerializeField] private float fadeInDuration = 0.5f;    // ���̵��� �ð�
    [SerializeField] private float fadeOutDuration = 0.3f;   // ���̵�ƿ� �ð�

    [Header("�˾� �ɼ�")]
    [SerializeField] private bool autoClose = false;         // �ڵ� �ݱ� ����
    [SerializeField] private float autoCloseDelay = 5f;      // �ڵ� �ݱ� �ð�
    [SerializeField] private bool showOnlyOnce = true;       // �� ���� ǥ�� ����

    private CanvasGroup canvasGroup;
    private bool hasShown = false;

    void Start()
    {
        // �ʱ� ����
        InitializePopup();

        // 10�� �� �˾� ǥ��
        StartCoroutine(ShowPopupAfterDelay());
    }

    void InitializePopup()
    {
        // CanvasGroup ������Ʈ Ȯ�� �� �߰�
        if (popupPanel != null)
        {
            canvasGroup = popupPanel.GetComponent<CanvasGroup>();
            if (canvasGroup == null)
            {
                canvasGroup = popupPanel.AddComponent<CanvasGroup>();
            }
        }

        // �˾� �г� �ʱ� ���� ���� (��Ȱ��ȭ)
        if (popupPanel != null)
        {
            popupPanel.SetActive(false);
            canvasGroup.alpha = 0f;
        }

        // �ݱ� ��ư �̺�Ʈ ����
        if (closeButton != null)
        {
            closeButton.onClick.AddListener(ClosePopup);
        }
    }

    IEnumerator ShowPopupAfterDelay()
    {
        // �� ���� ǥ�� �ɼ��� �����ְ� �̹� ǥ���ߴٸ� �ߴ�
        if (showOnlyOnce && hasShown)
            yield break;

        // ������ �ð���ŭ ���
        yield return new WaitForSeconds(delayTime);

        // �˾� ǥ��
        ShowPopup();

        // �ڵ� �ݱⰡ Ȱ��ȭ�Ǿ� �ִٸ�
        if (autoClose)
        {
            yield return new WaitForSeconds(autoCloseDelay);
            ClosePopup();
        }
    }

    public void ShowPopup()
    {
        if (popupPanel == null) return;

        // �˾� �г� Ȱ��ȭ
        popupPanel.SetActive(true);
        hasShown = true;

        // ���̵��� �ִϸ��̼�
        StartCoroutine(FadeIn());

        Debug.Log("�˾��� ǥ�õǾ����ϴ�!");
    }

    public void ClosePopup()
    {
        if (popupPanel == null) return;

        // ���̵�ƿ� �ִϸ��̼� �� ��Ȱ��ȭ
        StartCoroutine(FadeOutAndHide());

        Debug.Log("�˾��� �������ϴ�!");
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

    // �ܺο��� �˾��� ��� ǥ���ϰ� ���� �� ���
    public void ShowPopupImmediately()
    {
        StopAllCoroutines();
        ShowPopup();
    }

    // �ܺο��� Ÿ�̸Ӹ� �����ϰ� ���� �� ���
    public void ResetTimer()
    {
        StopAllCoroutines();
        hasShown = false;
        StartCoroutine(ShowPopupAfterDelay());
    }

    void OnDestroy()
    {
        // �޸� ���� ������ ���� �̺�Ʈ ������ ����
        if (closeButton != null)
        {
            closeButton.onClick.RemoveListener(ClosePopup);
        }
    }
}

// ���� �ּ�:
/*
1. �� GameObject�� �����ϰ� �� ��ũ��Ʈ�� ���Դϴ�.
2. UI Canvas �ȿ� �˾��� Panel�� �����մϴ�.
3. Panel �ȿ� Image�� Button(�ݱ��)�� ��ġ�մϴ�.
4. Inspector���� ������ �����մϴ�:
   - Popup Panel: �˾� �г� GameObject
   - Popup Image: ǥ���� �̹��� ������Ʈ
   - Close Button: �ݱ� ��ư ������Ʈ
5. Delay Time�� 10���� ���� (�⺻��)
6. �ʿ信 ���� �ٸ� �ɼǵ鵵 �����մϴ�.

���û���:
- �˾� �гο� CanvasGroup ������Ʈ�� �̸� �߰��ϸ� �� �ε巯�� �ִϸ��̼��� �����մϴ�.
- ����� ��Ӱ� �Ϸ��� Panel �ڿ� ��ο� Image�� �߰��ϼ���.
*/


