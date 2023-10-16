using System.Collections.Generic;
using UnityEngine;

namespace Characters.Config
{
    public class CharactersEntity : ScriptableObject, ICharactersEntity
    {
        [SerializeField] private List<CharacterConfig> _characterConfigs;
        
        public CharacterConfig GetCharacterConfig(CharacterType characterType)
        {
            foreach (var character in _characterConfigs)
            {
                if (character.IsCharacterType(characterType)) return character;
            }
            return null;
        }
    }
}