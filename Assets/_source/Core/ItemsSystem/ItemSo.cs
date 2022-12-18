using DevourDev.Unity.ScriptableObjects;
using DevourDev.Unity.Utils;
using UnityEngine;

namespace Game.Core.ItemsSystem
{
    [CreateAssetMenu(menuName = "Game/Items/Item")]
    public class ItemSo : SoDatabaseElement
    {
        [SerializeField] private MetaInfo _metaInfo;

        //stackable/nonstackable
        //on equip actions
        //on dequip actions
        //auras?
        //weight?
        //avg cost?
        //rareness?
        //etc...

        public MetaInfo MetaInfo => _metaInfo;
    }
}
