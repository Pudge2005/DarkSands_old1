using System;
using UnityEngine;

namespace Game.Core.Teams
{
    [Obsolete("неочевидные взаимоотношения", false)]
    public sealed class DiplomacyOld : ScriptableObject
    {
        [System.Serializable]
        private class Relations
        {
            [System.Serializable]
            public class RelationsGroup
            {
                [SerializeField] private DiplomacyAttitude _relations;
                [SerializeField] private TeamSo[] _teams;

                public RelationsGroup(DiplomacyAttitude relations, TeamSo[] teams)
                {
                    _relations = relations;
                    _teams = teams;
                }

                public DiplomacyAttitude Relations => _relations;
                public TeamSo[] Teams => _teams;
            }


            [SerializeField] private TeamSo _team;
            [SerializeField] private RelationsGroup[] _relationsGroups;


            public Relations(TeamSo team, RelationsGroup[] relationsGroups)
            {
                _team = team;
                _relationsGroups = relationsGroups;
            }


            public TeamSo Team => _team;
            public RelationsGroup[] RelationsGroups => _relationsGroups;
        }


        [SerializeField] private Relations[] _relations;
        [SerializeField] private DiplomacyAttitude _defaultRelations;


        public DiplomacyAttitude GetTeamsRelations(TeamSo teamA, TeamSo teamB)
        {
            foreach (var r in _relations)
            {
                if (r.Team != teamA && r.Team != teamB)
                    continue;

                if (r.Team != teamA)
                    (teamA, teamB) = (teamB, teamA);

                foreach (var group in r.RelationsGroups)
                {
                    foreach (var t in group.Teams)
                    {
                        if (t == teamB)
                            return group.Relations;
                    }
                }
            }

            return _defaultRelations;
        }
    }
}
