using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class SpecialDeliveryView : MonoBehaviour
{
    [Inject] private ISpecialDelivery SpecialDelivery { get; }
    [Inject] private Text Text { get; }

    [SerializeField] private string prefix;

    private void Update()
    {
        if (SpecialDelivery.Source != null)
        {
            Text.text = $"{prefix}\n  {SpecialDelivery.Name}\n  Destination {SpecialDelivery.Destination}";
        }
        else
        {
            Text.text = $"{prefix}\n  Pickup from {SpecialDelivery.Destination}";
        }
    }
}
