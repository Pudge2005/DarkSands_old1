using DevourDev.Unity.International;
using UnityEngine;

namespace DevourDev.Unity.Utils
{
    [System.Serializable]
    public class MetaInfo
    {
        [SerializeField] private MultiCulturalItem<string> _name;
        [SerializeField] private Sprite _icon;
        [SerializeField] private Sprite _preview;

        public MultiCulturalItem<string> Name => _name;
        public Sprite Icon => _icon;
        public Sprite Preview => _preview;
    }
}
