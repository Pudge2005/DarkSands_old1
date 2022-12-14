using System;
using System.Collections.Generic;
using System.Linq;
using Game.Core.Characters;
using UnityEngine;

namespace Game.Core.TwoDInThreeD
{
    public class TwoDVisualRotator : MonoBehaviour
    {
        [System.Serializable]
        private class VisualRotation : IEquatable<DirectionSo>
        {
            [SerializeField] private DirectionSo _direction;
            [SerializeField] private GameObject _visual;
            [SerializeField] private bool _default;

            private Transform _visualsParent;
            private GameObject _visualInstance;


            public GameObject VisualInstance
            {
                get
                {
                    if (_default)
                        return _visual;

                    if (_visualInstance == null)
                    {
                        UnityEngine.Debug.Log(_visualsParent);
                        _visualInstance = Instantiate(_visual, _visualsParent);
                    }

                    return _visualInstance;
                }
            }


            public void SetParent(Transform tr)
            {
                _visualsParent = tr;
            }

            public override bool Equals(object obj)
            {
                return obj is VisualRotation rotation &&
                       EqualityComparer<DirectionSo>.Default.Equals(_direction, rotation._direction);
            }

            public override int GetHashCode()
            {
                return _direction.GetInstanceID();
            }

            public bool Equals(DirectionSo other)
            {
                return other.GetInstanceID() == _direction.GetInstanceID(); //can throw null ref ex
            }

            public static implicit operator DirectionSo(VisualRotation v)
            {
                return v._direction;
            }

        }


        [SerializeField] private Character _character;
        [SerializeField] private VisualRotation[] _directions;
        [SerializeField] private Transform _visualParent;

        private GameObject _activeVisual;


        private void Start()
        {
            Debug.Log($"directions count: {_directions.Length}");

            foreach (var dir in _directions)
            {
                dir.SetParent(_visualParent);
            }
            _character.OnFacingDirectionChanged += HandleFacingDirectionChanged;
            HandleFacingDirectionChanged(_character, _character.FacingDirection, _character.FacingDirection);
        }

        private void HandleFacingDirectionChanged(Character character, DirectionSo from, DirectionSo to)
        {
            int index = Array.IndexOf(_directions, to);

            if (index < 0)
            {
                index = IndexOfClosestTo(to);
            }

            if (_activeVisual != null)
                _activeVisual.SetActive(false);

            _activeVisual = _directions[index].VisualInstance;
            _activeVisual.SetActive(true);
        }

        private int IndexOfClosestTo(DirectionSo target)
        {
            float closestD = float.PositiveInfinity;
            int closestIndex = -1;
            Vector3 targetVector = target.VectorDirection;

            for (int i = 0; i < _directions.Length; i++)
            {
                Vector3 v = ((DirectionSo)_directions[i]).VectorDirection;

                float d = (v - targetVector).sqrMagnitude;

                if (d < closestD)
                {
                    closestD = d;
                    closestIndex = i;
                }
            }

            return closestIndex;
        }
    }
}
