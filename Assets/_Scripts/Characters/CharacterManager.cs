using System;
using _Scripts.Initialization;
using Characters.Config;
using UnityEngine;
namespace Characters
{
    public class CharacterManager : MonoBehaviour
    {
        [SerializeField] private CharacterType _characterType;
        [SerializeField] private int _skinIndex = 0;

        private CharacterConfig _characterConfig;
        private void Start()
        {
            InitializeCharacterConfig();
            CreateCharacterModel();
        }

        private void InitializeCharacterConfig()
        {
            _characterConfig = ConfigInstaller.Instance.CharactersEntity.GetCharacterConfig(_characterType);
        }

        private void CreateCharacterModel()
        {
            var model = Instantiate(_characterConfig.Prefab, transform.parent);
            var characterSingleManager = model.GetComponent<CharacterSingleManager>();
            var texture = _characterConfig.Textures[_skinIndex];
            characterSingleManager.SetRenderer(texture);
        }
    }
}