namespace BusinessManager;

public abstract class InputExсeption
{
    public static int Catch(int menuItemsQuantity) 
    {
        var inputDigit = 0;
        var inputExceptionCatch = true;
        const string exception = "\nYou entered an inappropriate character, please try again.\n";
        
        while (inputExceptionCatch)
        {
            try
            {
                inputDigit = Convert.ToInt32(Console.ReadLine());
            }
            catch (Exception)
            {
                Console.WriteLine(exception);
                continue;
            }
    
            if (inputDigit > 0 && inputDigit < menuItemsQuantity + 1)
            {
                inputExceptionCatch = false;
            }
            else
            {
                Console.WriteLine(exception);
            }
        }
        
        return inputDigit - 1;
    }
}