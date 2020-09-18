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
    [Inject] private ICargo Cargo { get; }

    private ICommodity CurrentCargo => Cargo.Commodity;
    public IOffer Offer { get; } = new Offer();

    public void Enter(ILocation location)
    {
        if (CargoHoldsAreFull())
        {
            OfferToSellCargoTo(location);
        }
        else
        {
            OfferToBuyCargoFrom(location);
        }
    }

    private void OfferToBuyCargoFrom(ILocation location)
    {
        var commodityForSale = location.GetBestSale();
        if (account.Balance >= commodityForSale.Value)
        {
            Offer.Value = -commodityForSale.Value;
            Offer.Commodity = commodityForSale;
            Offer.Description = $"{commodityForSale.Name} is being sold for {commodityForSale.Value} € here.";
        }
    }

    private bool CargoHoldsAreFull()
    {
        return Cargo.IsFull;
    }

    private void OfferToSellCargoTo(ILocation location)
    {
        var offer = location.GetImportOfferFor(CurrentCargo);
        Offer.Value = offer;
        var offerDescription = $"Traders at {location} are willing to buy your {CurrentCargo.Name} for {Offer.Value}.";
        {
            Offer.Value = offer;
            Offer.Commodity = CurrentCargo;
            Offer.Description = offerDescription;
        }
    }

    public void AcceptOffer()
    {
        if (Offer.IsAvailable)
        {
            market.RegisterTransaction(Offer.Commodity);
            account.Credit(Offer.Value);

            if (Offer.IsAPurchase)
            {
                LoadCargo(Offer.Commodity);
            }
            else if (Offer.IsASale)
            {
                UnloadCargo();
            }

            Offer.Clear();
        }
    }

    private void UnloadCargo()
    {
        Cargo.Unload();
    }

    private void LoadCargo(ICommodity offerCommodity)
    {
        Cargo.Load(offerCommodity);
    }

    public void Exit()
    {
        ClearOffer();
    }

    private void ClearOffer()
    {
        Offer.Clear();
    }
}
