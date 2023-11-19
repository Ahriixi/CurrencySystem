# CurrencySystem
Define your own currencies and convert between them with the right factor

## What does the CurrencySystem do?
The CurrencySystem allows you to define your own currencies and convert between them with the right factor. The same principle is used in World of Warcraft with "Copper", "Silver" and "Gold".

## Example
Here is an example on how to use the CurrencySystem.

```cs
string[] currencyNames = new string[] { "Copper", "Silver", "Gold" };

int[] conversionFactors = new int[] {100};

Currency currency = new Currency(currencyNames, conversionFactors);

currency.SetBaseCurrencyValue(8353);

string currencyString = currency.ToStringReversed(); // "83 Gold 53 Silver 0 Copper"
```

## Get started
Be sure to use .NET 7.0 or higher.

### Clone the repository
```bash
git clone https://github.com/ahriixi/currencysystem
```

### Build the project
```bash
dotnet build
```

You're ready to go! Just link the CurrencySystem.dll to your project and you're ready to go.

## Documentation
You can find the full documentation [here](https://github.com/Ahriixi/CurrencySystem/wiki).

## Contributing
Know how to make this project better or found a bug? Send a PR!
