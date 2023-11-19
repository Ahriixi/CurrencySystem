namespace Ahriixi.CurrencySystem
{
    public class Currency
    {
        /// <summary>
        /// The names of the currencies.
        /// </summary>
        private readonly string[] _currencyNames;
        
        /// <summary>
        /// The values of the currencies.
        /// </summary>
        internal readonly int[] CurrencyValues;
        
        /// <summary>
        /// The factor to convert one currency into another.
        /// </summary>
        private readonly int[] _convertFactors;

        private bool _isDirty = false;
        
        /// <summary>
        /// The amount of currencies.
        /// </summary>
        public int AmountOfCurrencies => CurrencyValues.Length;
        
        /// <summary>
        /// Every currency value in the order of the currency names.
        /// </summary>
        public int[] AllValues => CurrencyValues;
        
        public int this[int index]
        {
            get => CurrencyValues[index];
            set
            {
                CurrencyValues[index] = value;
                _isDirty = true;
                SplitUpMoney();
            } 
        }

        /// <summary>
        /// Saves the currency names and the convert factor
        /// </summary>
        /// <param name="names">The names for the currencies</param>
        /// <param name="convertFactors">The convert factors to convert one currency into another.</param>
        /// /// <exception cref="InvalidOperationException"></exception>
        public Currency(string[] names, params int[] convertFactors)
        {
            _currencyNames = names;
            CurrencyValues = new int[names.Length];

            if (convertFactors.Length >= AmountOfCurrencies)
            {
                throw new InvalidOperationException("Too many factors.");
            }

            for (int i = 0; i < _currencyNames.Length; i++)
            {
                CurrencyValues[i] = 0;
            }
            _convertFactors = convertFactors;
        }
        
        /// <summary>
        ///  Saves the currency names, the values and the convert factor
        /// </summary>
        /// <param name="names">The currency names</param>
        /// <param name="values">The current values for currencies.</param>
        /// <param name="convertFactors">The conversion factors for the currencies.</param>
        /// <exception cref="InvalidOperationException">If there are more conversion factors than currencies.</exception>
        public Currency(string[] names, int[] values, params int[] convertFactors)
        {
            _currencyNames = names;
            CurrencyValues = values;
            _isDirty = true;

            if (convertFactors.Length >= AmountOfCurrencies)
            {
                throw new InvalidOperationException("Too many factors.");
            }

            _convertFactors = convertFactors;
            SplitUpMoney();
        }

        /// <summary>
        /// Splits up the money into the different currencies.
        /// </summary>
        private bool SplitUpMoney()
        {
            if(!_isDirty) return false;
            
            if (_currencyNames.Length == 1) return false;

            Converter converter = new Converter(_convertFactors);
            return converter.ConvertMoney(this);

        }

        /// <summary>
        /// Sets the amount of money.
        /// </summary>
        /// <param name="amount">The amount to be added</param>
        public void SetBaseCurrencyValue(int amount)
        {
            CurrencyValues[0] = amount;
            _isDirty = true;
            SplitUpMoney();
        }
        
        /// <summary>
        /// Returns the amount of money. The first currency name added will be the first to mention.
        /// </summary>
        /// <returns>Returns the amount of money.</returns>
        public override string ToString()
        {
            SplitUpMoney();
            string output = "";
            for (int i = 0; i < AmountOfCurrencies; i++)
            {
                output += $"{_currencyNames[i]}: {CurrencyValues[i]}";
            }

            return output;
        }
        
        /// <summary>
        /// Returns the amount of money. The last currency name added will be the first to mention.
        /// </summary>
        /// <returns>Returns the amount of money.</returns>
        public string ToStringReversed()
        {
            SplitUpMoney();
            string output = "";
            for (int i = AmountOfCurrencies-1; i > -1; i--)
            {
                output += $"{_currencyNames[i]}: {CurrencyValues[i]}";
            }

            return output;
        }
    }
}
