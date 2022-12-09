using UnityEngine;

namespace DevourDev.Unity.ScriptableObjects
{
    public abstract class SoDatabaseElement : ScriptableObject
    {
        [HideInInspector, SerializeField] internal int _soDatabaseElementID;


        public int GetDatabaseElementID => _soDatabaseElementID;
    }
}