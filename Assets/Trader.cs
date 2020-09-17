using Zenject;

public interface ITrader
{
    void Enter(ILocation location);
    void Exit();
    void AcceptOffer();
    IOffer Offer { get; }
}

public class Trader : ITrader
{
    [Inject] private IBankAccount account;
    [Inject] private IMarket market;

    private ICommodity CurrentCargo { get; set; }
    public IOffer Offer { get; } = new Offer();

    public void Enter(ILocation location)
    {
        if (CurrentCargo != null)
        {
            GetDealFrom(location);
        }
        else
        {
            var commodityForSale = location.GetBestSale();
            if (account.Balance >= commodityForSale.Value)
            {
                Offer.Value = -commodityForSale.Value;
                Offer.Description = $"{commodityForSale.Name} is being sold for {commodityForSale.Value} € here.";
            }
        }
    }

    private void GetDealFrom(ILocation location)
    {
        Offer.Value = location.GetDealFor(CurrentCargo);
        if (Offer.Value > 0)
        {
            Offer.Description = $"Traders at {location} are willing to buy your {CurrentCargo.Name} for {Offer.Value}.";
        }
    }

    public void AcceptOffer()
    {
        if (Offer.Value != 0)
        {
            account.Credit(Offer.Value);
            market.RegisterTransaction(CurrentCargo);
            Offer.Value = 0;
            CurrentCargo = null;
        }
    }

    public void Exit()
    {
        Offer.Value = 0;
    }
}
