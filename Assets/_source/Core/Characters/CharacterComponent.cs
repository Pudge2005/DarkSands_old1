using System.Threading.Tasks;
using UnityEngine;

namespace Game.Core.Characters
{
    public abstract class CharacterComponent : MonoBehaviour
    {
        private Character _character;


        public Character Character => _character;


        internal void SetCharacter(Character character)
        {
            _character = character;
            InitCharacterComponent();
        }


        protected virtual void InitCharacterComponent() { }
    }
}
