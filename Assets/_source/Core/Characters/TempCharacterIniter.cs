using UnityEngine;

namespace Game.Core.Characters
{
    public sealed class TempCharacterIniter : MonoBehaviour
    {
        [SerializeField] private CharacterSo _characterSo;
        [SerializeField] private Character _character;


        private void Awake()
        {
            _character.InitCharacter(_characterSo);
        }
    }
}
