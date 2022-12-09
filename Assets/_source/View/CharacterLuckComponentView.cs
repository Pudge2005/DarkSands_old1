using DevourDev.Unity.International;
using DevourDev.Unity.Utils;
using UnityEngine;

namespace Game.Core.Characters
{
    [RequireComponent(typeof(CharacterLuckComponent))]
    public sealed class CharacterLuckComponentView : MonoBehaviour
    {
        private const string _luckInterpolatingKey = "<luck>";

        [SerializeField] private PoppingText3D _poppingLuckPrefab;
        [SerializeField] private Gradient _luckGradient;
        [Tooltip(_luckInterpolatingKey)]
        [SerializeField] private MultiCulturalString _luckText;

        private CharacterLuckComponent _luck;


        private void Start()
        {
            _luck = GetComponent<CharacterLuckComponent>();
            _luck.OnDiceRolled += HandleDiceRolled;
        }

        private void HandleDiceRolled(float luck)
        {
            var pop = Instantiate(_poppingLuckPrefab);
            pop.transform.position = transform.position + Vector3.up;
            pop.SetText(_luckText.Get().Replace(_luckInterpolatingKey, (luck * 100f).ToString("N0")));
            pop.SetColor(_luckGradient.Evaluate(luck));
            pop.Init();
        }
    }
}
