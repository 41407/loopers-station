using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class TraderView : MonoBehaviour
{
    [Inject] private ITrader trader;
    [Inject] private Text text;

    private void Update()
    {
        if (trader.Offer?.Value != 0)
        {
            text.text = trader.Offer.Description;
        }
    }
}
