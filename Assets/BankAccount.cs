internal interface IBankAccount
{
    int Balance { get; }
    void Credit(int sum);
}

public class BankAccount : IBankAccount
{
    public int Balance { get; private set; }

    public void Credit(int sum)
    {
        Balance += sum;
    }
}
