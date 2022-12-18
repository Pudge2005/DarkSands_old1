namespace DevourDev.Base.Economics
{
    public class ValuesToStringCustomConverter
    {
        private readonly string _thouthandPfx;
        private readonly string _millionPfx;
        private readonly string _billionPfx;
        private readonly string _trillionPfx;
        private readonly string _quadrillionPfx;

        private const long _thouthandLong = 1000;
        private const long _millionLong = 1000_000;
        private const long _billionLong = 1000_000_000;
        private const long _trillionLong = 1000_000_000_000;
        private const long _quadrillionLong = 1000_000_000_000_000;


        public ValuesToStringCustomConverter(string thouthandPfx, string millionPfx, string billionPfx, string trillionPfx, string quadrillionPfx)
        {
            _thouthandPfx = thouthandPfx;
            _millionPfx = millionPfx;
            _billionPfx = billionPfx;
            _trillionPfx = trillionPfx;
            _quadrillionPfx = quadrillionPfx;
        }


        public string NumToString(long num)
        {
            return num switch
            {
                > _quadrillionLong => ((double)num / _quadrillionLong).ToString("N1") + _quadrillionPfx,
                > _trillionLong => ((double)num / _trillionLong).ToString("N1") + _trillionPfx,
                > _billionLong => ((double)num / _billionLong).ToString("N1") + _billionPfx,
                > _millionLong => ((double)num / _millionLong).ToString("N1") + _millionPfx,
                > _thouthandLong => ((double)num / _thouthandLong).ToString("N1") + _thouthandPfx,
                _ => $"{num}",
            };
        }

        public string NumToString(int num)
        {
            uint unum = (uint)num;
            return unum switch
            {
                > (uint)_billionLong => ((double)unum / _billionLong).ToString("N1") + _billionPfx,
                > (uint)_millionLong => ((double)unum / _millionLong).ToString("N1") + _millionPfx,
                > (uint)_thouthandLong => ((double)unum / _thouthandLong).ToString("N1") + _thouthandPfx,
                _ => $"{unum}",
            };
        }
    }
}
