using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class CreditsView : MonoBehaviour
{
    [Inject] private IBankAccount Account { get; }
    [Inject] private Text text;
    [SerializeField] private string suffix;

    private int CurrentCredits { get; set; }

    private void Update()
    {
        if (CurrentCredits < Account.Balance)
        {
            CurrentCredits++;
            UpdateView();
        }
    }

    private void UpdateView()
    {
        text.text = $"{CurrentCredits} {suffix}";
    }
}
