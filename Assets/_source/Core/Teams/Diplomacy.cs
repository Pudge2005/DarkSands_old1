using System;
using UnityEngine;

namespace Game.Core.Teams
{
    [CreateAssetMenu(menuName = "Game/Teams/Diplomacy")]
    public sealed class Diplomacy : ScriptableObject
    {
        [System.Serializable]
        private class OneSidedAttitude
        {
            [SerializeField] private TeamSo _teamA;
            [SerializeField] private DiplomacyAttitude _attitude;
            [SerializeField] private TeamSo _teamB;


            public OneSidedAttitude(TeamSo teamA, DiplomacyAttitude attitude, TeamSo teamB)
            {
                _teamA = teamA;
                _attitude = attitude;
                _teamB = teamB;
            }


            public TeamSo TeamA => _teamA;
            public DiplomacyAttitude Attitude => _attitude;
            public TeamSo TeamB => _teamB;
        }


        [SerializeField] private OneSidedAttitude[] _relations;
#if UNITY_EDITOR
        [SerializeField] private bool _sort;
        [SerializeField] private string _message;
#endif
        [SerializeField] private DiplomacyAttitude _defaultAttitude;


#if UNITY_EDITOR
        private void OnValidate()
        {
            if (_sort)
            {
                _sort = false;
                Array.Sort(_relations, (a, b) => a.TeamA.name.CompareTo(b.TeamA.name));

                Span<int> duplicates = stackalloc int[_relations.Length];
                int duplicatesCount = 0;

                OneSidedAttitude prev = null;

                for (int i = 0; i < _relations.Length; i++)
                {
                    OneSidedAttitude item = _relations[i];
                    if (prev == item)
                    {
                        duplicates[duplicatesCount++] = i;
                    }

                    prev = item;
                }

                if (duplicatesCount > 0)
                {
                    UnityEngine.Debug.LogError("=======");

                    foreach (var ind in duplicates)
                        UnityEngine.Debug.LogError($"duplicating element index: {ind}");

                    UnityEngine.Debug.LogError("=======");

                    _message = $"found {duplicatesCount} duplicates. Indexes printed to console.";
                }
                else
                {
                    _message = $"Success";
                }

                UnityEditor.EditorUtility.SetDirty(this);
            }
        }
#endif


        /// <returns>Attitude of Team A to Team B</returns>
        public DiplomacyAttitude GetOneSidedAttitude(TeamSo teamA, TeamSo teamB)
        {
            foreach (var att in _relations)
            {
                if (att.TeamA == teamA && att.TeamB == teamB)
                    return att.Attitude;
            }

            return _defaultAttitude;
        }
    }
}
