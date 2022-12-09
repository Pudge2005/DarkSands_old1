using UnityEngine;

namespace DevourDev.Unity.International
{
    [System.Serializable]
    public class Translation<TItem>
    {
        [SerializeField] private CultureObject _culture;
        [SerializeField] private TItem _item;


        public Translation(CultureObject culture, TItem item)
        {
            _culture = culture;
            _item = item;
        }


        public CultureObject Culture => _culture;
        public TItem Item => _item;
    }
}
