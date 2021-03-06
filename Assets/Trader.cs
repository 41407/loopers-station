using Zenject;

public interface ITrader
{
    void Enter(ILocation location);
    void Exit();
    void AcceptOffer();
    IOffer Offer { get; }
    ILocation CurrentLocation { get; }
    void Enter(IArea area);
}

public class Trader : ITrader
{
    [Inject] private IBankAccount account;
    [Inject] private ICargo Cargo { get; }

    private IMarket CurrentMarket { get; set; }
    public ILocation CurrentLocation { get; set; }


    private ICommodity CurrentCargo => Cargo.Commodity;
    public IOffer Offer { get; } = new Offer();

    public void Enter(IArea area)
    {
        CurrentMarket = area.Market;
    }

    public void Enter(ILocation location)
    {
        CurrentLocation = location;
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
            Offer.Description = $"{commodityForSale.Name} is being sold for {commodityForSale.Value} € here.\n\nx - buy {commodityForSale.Name}";
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
        var offerDescription = $"Traders at {location} are willing to buy your {CurrentCargo.Name} for {Offer.Value}.\n\nx - sell {CurrentCargo.Name}";
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
            CurrentMarket.RegisterTransaction(Offer.Commodity);
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
        Cargo.UnloadCommodity();
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
