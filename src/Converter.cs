namespace Ahriixi.CurrencySystem
{
    internal class Converter
    {
        int[] _convertFactors;

        /// <summary>
        /// Sets up the converter with the convert factors.
        /// </summary>
        /// <param name="convertFactors">The factors to be added.</param>
        /// <exception cref="InvalidOperationException">If one factor is zero (0).</exception>
        internal Converter(params int[] convertFactors)
        {
            for (int i = 0; i < convertFactors.Length; i++)
            {
                if (convertFactors[i] == 0)
                {
                    throw new InvalidOperationException("A factor cannot be 0.");
                }
            }
            int[] convertFactor = convertFactors;
            
            // If there is only one factor, fill the entire factor array with the only factor.
            if(convertFactors.Length == 1) 
            {
                FillArrayWithOneValue(convertFactors[0], convertFactor);
            }

            _convertFactors = convertFactor;
        }

        /// <summary>
        /// Converts the money into the different currencies. The money is stored in the Currency object.
        /// </summary>
        /// <param name="currency">The affected Currency Object</param>
        /// <returns><see langword="true"/>If conversion was successful, else <see langword="false"/>.</returns>
        internal bool ConvertMoney(Currency currency)
        {
            if (CheckIfNoValuesStored(currency)) return false;
            
            if (CheckIfThereIsOnlyOneCurrency(currency)) return false;
            
            int[] factorsToUse = _convertFactors;

            if (factorsToUse.Length == 1)
            {
                factorsToUse = new int[currency.AmountOfCurrencies];
                FillArrayWithOneValue(_convertFactors[0], factorsToUse);
            }

            for (int i = 0; i < factorsToUse.Length - 1; i++)
            {
                int tmp = currency.CurrencyValues[i];

                currency.CurrencyValues[i] %= factorsToUse[i];
                currency.CurrencyValues[i + 1] += tmp / factorsToUse[i];
            }
            
            return true;
        }

        /// <summary>
        /// Checks if there are any values for the currencies stored in the Currency object.
        /// </summary>
        /// <param name="currency">The current Currency object</param>
        /// <returns><see langword="true"/> if there are no values, <see langword="false"/> if there are.</returns>
        private bool CheckIfNoValuesStored(Currency currency)
        {
            int AmountOfZeros = 0;

            for (int i = 0; i < currency.CurrencyValues.Length; i++)
            {
                if (currency.CurrencyValues[i] == 0)
                {
                    AmountOfZeros++;
                }
            }

            return AmountOfZeros == currency.CurrencyValues.Length;
        }
        
        private bool CheckIfThereIsOnlyOneCurrency(Currency currency) => currency.AmountOfCurrencies == 1;

        /// <summary>
        /// Fills an array with a given value.
        /// </summary>
        /// <param name="valueToUse">The value to fill the array with.</param>
        /// <param name="array">The array to be filled.</param>
        private void FillArrayWithOneValue<T>(T valueToUse, T[] array)
        {
            for(int i = 0; i < array.Length; i++)
            {
                array[i] = valueToUse;
            }
        }

    }
}
