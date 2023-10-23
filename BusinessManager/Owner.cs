namespace BusinessManager;

public class Owner
{
    private string _name;
    private Wallet _wallet;
    private List<Business> _businesses;
    public string Name => _name;
    
    public Owner(string name, int capital)
    {
        _name = name;
        _wallet = new Wallet(capital);
        _businesses = new List<Business>();
    }
    
    public void PutIncomeToWallet(int income)
    {
        _wallet.AddIncome(income);
    }

    public int GetCapital()
    {
        var capital = _wallet.OwnerCapital;
        return capital;
    }
    
    public void PrintBusinesses() 
    {
        if (IsZeroBusinesses()) return;
        
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"{_name} owns:");
        Console.ResetColor();
        
        for (var i = 0; i < _businesses.Count; i++)
        {
            Console.Write($"{i + 1}. ");
            _businesses[i].Print();
        } 
    }
    
    private void CheckPurchase(Business business)
    {
        if (!_wallet.IsMoneyEnough(business)) return;
        _wallet.SpendMoney(business.Price);
        business.ChangeLastReceivedTime(DateTime.Now);
        _businesses.Add(business);
            
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"\n{_name}, congratulations on a successful purchase!");
        Console.ResetColor();
    }

    public void BuyBusiness(BusinessManager businessManager)
    {
        if (!businessManager.GetAvailableBusiness(_businesses) && _businesses.Count != 0)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nThere are no available businesses for purchase.");
            Console.ResetColor();
            return;
        }
        
        Console.Write("\nEnter the number of the business you want to buy: ");
        
        var userChoice = InputExсeption.Catch(3);
        
        var businesses = businessManager.CheckAvailableBusinesses(_businesses);
        var business = businessManager.GetBusinessByIndex(userChoice, businesses);

        CheckPurchase(business);
    }
    
    public void UpgradeBusiness(BusinessUpgrades upgrades)
     {
         if (IsZeroBusinesses()) return;
         
         if (IsBusinessesUpgraded(upgrades))
         {
             Console.ForegroundColor = ConsoleColor.Yellow;
             Console.WriteLine("\nAll upgrades for your businesses have already been purchased.");
             Console.ResetColor();
             return;
         }
         
         PrintBusinessesForUpgrade(upgrades);
         
         var userChoice = InputExсeption.Catch(_businesses.Count);
         
         ShowBusinessUpgrades(userChoice, upgrades);
         
         PrintIncome();
     }

    private bool IsZeroBusinesses()
    {
        if (_businesses.Count != 0) return false;
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("\nThere is no business to upgrade.");
        Console.ResetColor();

        return true;
    }

    private void PrintBusinessesForUpgrade(BusinessUpgrades upgrades)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"\n{_name} owns the amount of: {_wallet.OwnerCapital}$.\n");
        Console.ResetColor();
        Console.WriteLine("Which business would you like to upgrade?\n");
        
        for (var i = 0; i < _businesses.Count; i++)
        {
            if (upgrades.GetAvailableBusinessUpgrade(_businesses[i]) == 0)
            {
                Console.Write($"{i + 1}. ");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("All upgrades have already been purchased! ");
                Console.ResetColor();
                _businesses[i].Print();
                continue;
            }
            Console.Write($"{i + 1}. "); _businesses[i].Print();
        }
        
        Console.Write("\nSelected business: ");
    }

    private void ShowBusinessUpgrades(int userChoice, BusinessUpgrades upgrades)
    {
        var businessUpgrades = upgrades.GetUpgradeList()
            .Where(business => business.GetType() == _businesses[userChoice].GetType())
            .Except(_businesses[userChoice].Upgrades).ToList();

        if (businessUpgrades.Count <= 0)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("\nAll upgrades for this business have already been purchased!\n");
            Console.ResetColor();
            return;
        }
        
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"\nUpgrades available for {_businesses[userChoice].Name}: ");
        Console.ResetColor();
        for (var i = 0; i < businessUpgrades.Count; i++)
        {
            Console.Write($"{i + 1}. ");
            businessUpgrades[i].Print();
        }
        
        Console.Write("\nEnter the upgrade number: ");
        
        var userUpgradeChoice = InputExсeption.Catch(businessUpgrades.Count);

        if (!_wallet.IsMoneyEnough(businessUpgrades[userUpgradeChoice])) return;
        
        BuyUpgrade(businessUpgrades, userChoice, userUpgradeChoice);
    }

    private void BuyUpgrade(List<Business> upgrades, int userIndex, int upgradeIndex)
    {
        var upgrade = upgrades[upgradeIndex];
        var upgradePrice = upgrades[upgradeIndex].Price;
        var upgradeIncome = upgrades[upgradeIndex].Income;
        _wallet.SpendMoney(upgradePrice);

        var businessToUpgrade = _businesses[userIndex];
        
        businessToUpgrade.ChangePrice(upgradePrice);
        businessToUpgrade.ChangeIncome(upgradeIncome);
        businessToUpgrade.AddUpgrade(upgrade);
        
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("\nBusiness successfully upgraded!");
        Console.ResetColor();
    }
    
    public void PrintIncome()
    {
        foreach (var business in _businesses)
        {
            business.CalculateIncome(this);
        }
    }

    private bool IsBusinessesUpgraded(BusinessUpgrades upgrades)
    {
        var isUpgraded = true;
        
        for (var i = 0; i < _businesses.Count; i++)
        {
            var separateUpgrades = upgrades.GetUpgradeList().Where(businesses => businesses.GetType() == _businesses[i].GetType())
                .Except(_businesses[i].Upgrades).ToList();

            if (separateUpgrades.Count == 0) continue;
            isUpgraded = false;
            return isUpgraded;
        }

        return isUpgraded;
    }
}