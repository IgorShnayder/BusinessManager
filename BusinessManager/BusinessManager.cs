namespace BusinessManager;

public class BusinessManager
{
    private List<Business> _gameAvailableBusiness;
    
    public BusinessManager()
    {
        _gameAvailableBusiness = new List<Business>();
        AddBusinessToGame();
    }
    
    private void AddBusinessToGame() 
    {
        _gameAvailableBusiness.Add(new Pizzeria("Pizzeria", 10000, 1500, 10f));
        _gameAvailableBusiness.Add(new AutoRepairShop("Auto Repair Shop", 6000, 500, 10f));
        _gameAvailableBusiness.Add(new Supermarket("Supermarket", 30000, 5000, 10f));
    }
    
    public List<Business> CheckAvailableBusinesses(List<Business> businesses)
    {
        var availableBusiness = _gameAvailableBusiness.Except(businesses).ToList();
        return availableBusiness;
    }

    public bool GetAvailableBusiness(List<Business> businesses)
    {
        var sameBusiness = _gameAvailableBusiness.Intersect(businesses).Count();
        
        if (businesses.Count != 0 && sameBusiness == _gameAvailableBusiness.Count)
        {
            return false;
        }

        var availableBusiness = CheckAvailableBusinesses(businesses);

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("\nAvailable businesses for purchase: \n");
        Console.ResetColor();
        
        for (var i = 0; i < availableBusiness.Count; i++)
        {
            Console.Write($"{i + 1}. "); 
            availableBusiness[i].Print();
        }
        
        return true;
    }
    
    public Business GetBusinessByIndex(int index, List<Business> businesses) 
    {
        var tempBusiness = businesses[index];
        return tempBusiness;
    }
}