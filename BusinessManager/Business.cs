namespace BusinessManager;

public class Business
{
    private readonly string _businessName;
    private int _price;
    private int _income;
    private readonly float _incomePeriod;
    private DateTime _lastReceivedTime;
    private List<Business> _ownerBusinessUpgrades;
    private int _timeMultiply;

    public int Price => _price;
    public int Income => _income;
    public List<Business> Upgrades => _ownerBusinessUpgrades;
    public string Name => _businessName;
    
    protected Business(string businessName, int price, int income, float incomePeriod)
    {
        _businessName = businessName;
        _price = price;
        _income = income;
        _incomePeriod = incomePeriod;
        _ownerBusinessUpgrades = new List<Business>();
    }

    public void Print()
    {
        Console.WriteLine($"{_businessName} | Price - {_price}$ | Income - {_income}$ | Brings in income every {_incomePeriod} seconds.");
    }
    
    public void ChangePrice(int price)
    {
        _price += price;
    }
    
    public void ChangeIncome(int income)
    {
        _income += income;
    }

    public void ChangeLastReceivedTime(DateTime time)
    {
        _lastReceivedTime = time;
    }
    
    public void CalculateIncome(Owner owner)
    {
        if (!IsReceiveIncome()) return;
        owner.PutIncomeToWallet(_income * _timeMultiply);
        
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"\n{owner.Name}, your business {_businessName} has brought you income - {_income * _timeMultiply}$.");
        Console.ResetColor();
    }

    private bool IsReceiveIncome()
    {
        var lastReceivedTime = DateTime.Now - _lastReceivedTime;
        if (lastReceivedTime.TotalSeconds >= _incomePeriod)
        {
            _lastReceivedTime = DateTime.Now;
            _timeMultiply = Convert.ToInt32(Math.Floor(lastReceivedTime.TotalSeconds / _incomePeriod));
            return true;
        }

        return false;
    }
    
    public void AddUpgrade(Business businessUpgrade)
    {
        _ownerBusinessUpgrades.Add(businessUpgrade);
    }
}