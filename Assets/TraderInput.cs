using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class TraderInput : MonoBehaviour
{
    [Inject] private IKeybindings keybindings;
    [Inject] private ITrader trader;

    private void Update()
    {
        if (TraderAcceptsOffer()) trader.AcceptOffer();
    }

    private bool TraderAcceptsOffer() => PlayerPressesAnyOf(keybindings.AcceptOffer);

    private static bool PlayerPressesAnyOf(IEnumerable<KeyCode> acceptOfferKey) => acceptOfferKey.Any(Input.GetKeyDown);
}
