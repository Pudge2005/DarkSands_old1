using System.Collections.Generic;

namespace DevourDev.International
{
    public class InternationalItem<TItem, TCulture> : IInternational<TItem, TCulture>
    {
        private readonly Dictionary<TCulture, TItem> _translations;


        public InternationalItem()
        {
            _translations = new();
        }


        public void AddTranslation(TCulture culture, TItem item)
        {
            _translations.Add(culture, item);
        }


        public TItem Get(TCulture culture)
        {
            _ = _translations.TryGetValue(culture, out var item);
            return item;
        }

        public bool TryGet(TCulture culture, out TItem item)
        {
            return _translations.TryGetValue(culture, out item);
        }
    }
}
