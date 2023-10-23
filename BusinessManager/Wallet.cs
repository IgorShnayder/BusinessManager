namespace BusinessManager;

public class Wallet
{
    private int _ownerCapital;
    public int OwnerCapital => _ownerCapital;

    public Wallet(int ownerCapital)
    {
        _ownerCapital = ownerCapital;
    }
    
    public void AddIncome(int income)
    {
        _ownerCapital += income;
    }

    public void SpendMoney(int price)
    {
        _ownerCapital -= price;
    }

    public bool IsMoneyEnough(Business business)
    {
        if (_ownerCapital >= business.Price) return true;
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("\nYou don't have enough money for this purchase!");
        Console.ResetColor();
        return false;
    }
}