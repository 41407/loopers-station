using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class CargoView : MonoBehaviour
{
    [Inject] private ICargo cargo;
    [Inject] private Text text;

    private void Update()
    {
        text.text = cargo.Description;
    }
}
