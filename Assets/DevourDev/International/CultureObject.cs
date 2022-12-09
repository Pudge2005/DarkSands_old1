using DevourDev.Unity.ScriptableObjects;
using DevourDev.Unity.Utils;
using UnityEngine;

namespace DevourDev.Unity.International
{
    [CreateAssetMenu(menuName = "DevourDev/International/Culture Object")]
    public class CultureObject : SoDatabaseElement
    {
        [SerializeField] private MetaInfo _metaInfo;


        public MetaInfo MetaInfo => _metaInfo;

    }
}
