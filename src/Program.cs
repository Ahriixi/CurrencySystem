namespace Ahriixi.CurrencySystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] names = { "Copper", "Silver", "Gold"};

            Currency currency = new Currency(names,100);
            currency.SetBaseCurrencyValue(78342);
            
            string amount = currency.ToString();
            
            Console.WriteLine(amount);
            Console.ReadLine();
        }
    }
}
