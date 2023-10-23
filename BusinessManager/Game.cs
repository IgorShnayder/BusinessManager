namespace BusinessManager;

public class Game
{
    private bool _shouldExitGame; 
    private const int _startCapital = 30000;
    
    private enum OwnerMenu
    {
        BuyBusiness,
        UpgradeBusiness,
        ExitGame 
    }
    
    public void StartGame()
    {
        Console.Write("\nPlease enter your name: ");
        var ownerName = Console.ReadLine();
        
        var owner = new Owner(ownerName, _startCapital);
        
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"\nHello, {ownerName}. Let's work successfully with your business!");
        Console.ResetColor();
        
        var businessManager = new BusinessManager();
        var businessesUpgrades = new BusinessUpgrades();

        while (!_shouldExitGame)
        {
            owner.PrintIncome();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n{owner.Name} owns the sum of: {owner.GetCapital()}$.");
            Console.ResetColor();
            owner.PrintBusinesses();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nChoose an action:");
            Console.ResetColor();
            Console.WriteLine("1. Buy a business");
            Console.WriteLine("2. Select a business to upgrade");
            Console.WriteLine("3. Exit the game\n");
            Console.Write("Please enter the action number: ");

            var userChoice = (OwnerMenu)InputExсeption.Catch(3);
            
            switch (userChoice)
            {
                case OwnerMenu.BuyBusiness:
                    owner.BuyBusiness(businessManager);               
                    break;
                
                case OwnerMenu.UpgradeBusiness:
                    owner.UpgradeBusiness(businessesUpgrades); 
                    break;

                case OwnerMenu.ExitGame:
                    ExitGame();
                    break;

                default:
                    Console.WriteLine("\nYou have selected an action not provided by the game. Please try again.");
                    break;
            }
        }
    }
    
    private void ExitGame()
    {
        _shouldExitGame = true;
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("\nAll the best!");
        Console.ResetColor();
    }
}