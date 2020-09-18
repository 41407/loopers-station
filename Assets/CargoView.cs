using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class CargoView : MonoBehaviour
{
    [Inject] private ICargo cargo;
    [Inject] private Text text;

    [SerializeField] private string header;

    private void Update()
    {
        text.text = $"{header}\n  {cargo.Description}";
    }
}
