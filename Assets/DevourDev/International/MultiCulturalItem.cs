using DevourDev.International;
using UnityEngine;

namespace DevourDev.Unity.International
{
    public static class International
    {
        public static CultureObject CurrentCulture { get; set; }
    }


    [System.Serializable]
    public class MultiCulturalItem<TItem> : IInternational<TItem, CultureObject>
    {
        [SerializeField] private Translation<TItem>[] _translations;


        public TItem Get(CultureObject culture)
        {
            foreach (var translation in _translations)
            {
                if (translation.Culture == culture)
                    return translation.Item;
            }

            return default;
        }

        public bool TryGet(CultureObject culture, out TItem item)
        {
            foreach (var translation in _translations)
            {
                if (translation.Culture == culture)
                {
                    item = translation.Item;
                    return true;
                }
            }

            item = default;
            return false;
        }

        public TItem Get()
        {
            if (TryGet(International.CurrentCulture, out var item))
                return item;

            if (_translations.Length > 0)
                return _translations[0].Item;

            return default;
        }
    }


    [System.Serializable]
    public class MultiCulturalString : MultiCulturalItem<string>
    {

    }
}
