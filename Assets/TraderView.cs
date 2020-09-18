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
            text.color = Color.white;
            UpdateText();
        }

        if (!string.IsNullOrEmpty(text.text) && !OfferIsAvailable())
        {
            text.color = Color.grey;
        }
    }

    private bool OfferIsAvailable() => trader.Offer.IsAvailable;

    private void UpdateText() => text.text = trader.Offer?.Description;
}
