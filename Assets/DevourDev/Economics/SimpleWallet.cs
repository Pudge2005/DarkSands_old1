namespace DevourDev.Base.Economics
{
    public class SimpleWallet
    {
        /// <summary>
        /// Wallet instance and delta
        /// </summary>
        public event System.Action<SimpleWallet, long> OnBalanceChanged;

        private long _balance;


        public SimpleWallet(long startBalance = 0)
        {
            _balance = startBalance;
        }


        public long Balance
        {
            get => _balance;
            set
            {
                _balance = value;
            }
        }


        public bool CanSpend(long v)
        {
            return (ulong)_balance >= (ulong)v;
        }

        public bool TrySpend(long v)
        {
            if (CanSpend(v))
            {
                Spend(v);
                return true;
            }

            return false;
        }

        public void Earn(long v)
        {
            ChangeBalance(v);
        }

        public void Spend(long v)
        {
            ChangeBalance(-v);
        }


        private void ChangeBalance(long delta)
        {
            _balance += delta;
            OnBalanceChanged?.Invoke(this, delta);
        }
    }
}
