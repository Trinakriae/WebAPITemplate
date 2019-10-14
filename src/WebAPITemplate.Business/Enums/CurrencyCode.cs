namespace WebAPITemplate.Business.Enums
{
    public class CurrencyCode
    {
        private CurrencyCode(string value) { Value = value; }

        public string Value { get; set; }

        public static CurrencyCode EUR { get { return new CurrencyCode("EUR"); } }
        public static CurrencyCode USD { get { return new CurrencyCode("USD"); } }
        public static CurrencyCode CAD { get { return new CurrencyCode("CAD"); } }
        public static CurrencyCode TRY { get { return new CurrencyCode("TRY"); } }
    }
}
