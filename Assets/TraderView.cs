using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class TraderView : MonoBehaviour
{
    [Inject] private ITrader trader;
    [Inject] private Text text;

    private void Update()
    {
        if (OfferIsAvailable())
        {
            UpdateText();
        }

        if (!string.IsNullOrEmpty(text.text) && !OfferIsAvailable())
        {
            text.text = "";
        }
    }

    private bool OfferIsAvailable() => trader.Offer?.Value != 0;

    private void UpdateText() => text.text = trader.Offer?.Description;
}
