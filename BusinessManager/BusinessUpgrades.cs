namespace BusinessManager;

public class BusinessUpgrades
{
    private List<Business> _businessesUpgrades;

    public BusinessUpgrades()
    {
        _businessesUpgrades = new List<Business>();
        AddBusinessesUpgrades();
    }
    
    private void AddBusinessesUpgrades()
    {
        _businessesUpgrades.Add(new Pizzeria("Add a summer terrace", 5000, 500, 10));
        _businessesUpgrades.Add(new Pizzeria("Improve the kitchen", 10000, 1400, 10));
        _businessesUpgrades.Add(new AutoRepairShop("Add another lift", 3000, 150, 10));
        _businessesUpgrades.Add(new AutoRepairShop("Upgrade the auto mechanics' equipment", 6000, 500, 10));
        _businessesUpgrades.Add(new AutoRepairShop("New service bay", 10000, 1000, 10));
        _businessesUpgrades.Add(new Supermarket("New equipment for the sales area", 10000, 3000, 10));
        _businessesUpgrades.Add(new Supermarket("Increased staff, employee training courses", 3500, 700, 10));
        _businessesUpgrades.Add(new Supermarket("Expanded range of products and delicacies", 8000, 2000, 10));
    }
    
    public int GetAvailableBusinessUpgrade(Business business)
    {
        var isSameUpgrades = _businessesUpgrades.Where(businesses => businesses.GetType() == business.GetType())
            .Except(business.Upgrades).ToList();
        return isSameUpgrades.Count;
    }

    public List<Business> GetUpgradeList()
    {
        return _businessesUpgrades;
    }
}