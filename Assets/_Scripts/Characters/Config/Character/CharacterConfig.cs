using System;
using UnityEngine;

namespace Characters.Config.Character
{
    [Serializable]
    public class CharacterConfig
    {
        [SerializeField] private GameObject _prefab;
        [SerializeField] private Texture[] _textures;
        [SerializeField] private CharacterType _type;

        public GameObject Prefab => _prefab;
        public Texture[] Textures => _textures;
        public CharacterType CharacterType => _type;
        public bool IsCharacterType(CharacterType type)
        {
            return _type == type;
        }
        
        public override string ToString()
        {
            return $"{_prefab.name} - {_textures.Length} -{_type.ToString()}";
        }
    }
}