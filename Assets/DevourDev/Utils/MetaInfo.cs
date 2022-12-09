using DevourDev.Unity.International;
using UnityEngine;

namespace DevourDev.Unity.Utils
{
    [System.Serializable]
    public class MetaInfo
    {
        [SerializeField] private MultiCulturalItem<string> _name;
        [SerializeField] private Texture2D _icon;
        [SerializeField] private Texture2D _preview;

        public MultiCulturalItem<string> Name => _name;
        public Texture2D Icon => _icon;
        public Texture2D Preview => _preview;
    }
}
