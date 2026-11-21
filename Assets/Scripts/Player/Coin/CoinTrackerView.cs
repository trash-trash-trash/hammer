using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CoinTrackerView : MonoBehaviour
{
    public CoinTracker coinTracker;

    public TMP_Text coinText;

    public Image coinImage;
    public float pulseScale = 1.3f;
    public float pulseDuration = 0.2f;

    private Vector3 originalScale;

    void Awake()
    {
        originalScale = coinImage.transform.localScale;
        coinTracker.AnnounceCoinCount += SetCoinText;
    }

    private void SetCoinText(int coinAmmount)
    {
        coinText.text = "x" + coinAmmount.ToString("D2");
        Pulse();
    }

    public void Pulse()
    {
        coinImage.transform.DOScale(originalScale * pulseScale, pulseDuration)
            .SetEase(Ease.OutBack)
            .OnComplete(() =>
                coinImage.transform.DOScale(originalScale, pulseDuration)
                    .SetEase(Ease.InOutSine)
            );
    }

    void OnDestroy()
    {
        coinTracker.AnnounceCoinCount -= SetCoinText;
    }
}