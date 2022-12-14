using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Core.Abilities
{
    [CreateAssetMenu(menuName = "Abilities/Cast Stages Settings")]
    public sealed class CastStagesSettingsSo : ScriptableObject
    {
        [SerializeField] private CastStageSo[] _order;


        //private Dictionary<CastStageSo, int> _internalDict;


#if UNITY_EDITOR
        private void OnValidate()
        {
            for (int i = 0; i < _order.Length; i++)
            {
                var stage = _order[i];
                stage.SetOrder(i);
                UnityEditor.EditorUtility.SetDirty(stage);
            }
        }
#endif

        //public IReadOnlyDictionary<CastStageSo, int> Order
        //{
        //    get
        //    {
        //        if (_internalDict == null)
        //        {
        //            var arr = _order;
        //            var c = arr.Length;
        //            _internalDict = new(c);
        //            var d = _internalDict;

        //            for (int i = -1; ++i < c;)
        //                d.Add(arr[i], i);
        //        }

        //        return _internalDict;
        //    }
        //}


        //public void Sort(CastStageSo[] arr)
        //{
        //    Array.Sort(arr, (l, r) => Order[l].CompareTo(Order[r]));
        //}
    }
}
