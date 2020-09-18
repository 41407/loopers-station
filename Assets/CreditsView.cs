using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class CreditsView : MonoBehaviour
{
    [Inject] private IBankAccount Account { get; }
    [Inject] private Text text;
    [SerializeField] private string suffix;

    private int CurrentCredits { get; set; }

    private void Start()
    {
        UpdateView();
    }

    private void Update()
    {
        if (AccountHasBeenCredited())
        {
            IncrementView();
        }
        else if (AccountHasLostCredit())
        {
            SetView(Account.Balance);
        }
    }

    private void SetView(int accountBalance)
    {
        CurrentCredits = accountBalance;
        UpdateView();
    }

    private void IncrementView()
    {
        CurrentCredits++;
        SetView(CurrentCredits);
    }

    private bool AccountHasLostCredit()
    {
        return CurrentCredits > Account.Balance;
    }

    private bool AccountHasBeenCredited()
    {
        return CurrentCredits < Account.Balance;
    }

    private void UpdateView()
    {
        text.text = $"{CurrentCredits} {suffix}";
    }
}
