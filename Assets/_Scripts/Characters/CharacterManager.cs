using System;
using _Scripts.Initialization;
using Characters.Config.Character;
using UnityEngine;
namespace Characters
{
    public class CharacterManager : MonoBehaviour
    {
        [SerializeField] private CharacterType _characterType;
        [SerializeField] private int _skinIndex = 0;
        
        private CharacterConfig _characterConfig;
        private CharacterSingleManager _characterSingleManager;
        private void Start()
        {
            InitializeCharacterConfig();
            CreateCharacterModel();
            SetTextureModel();
        }

        private void InitializeCharacterConfig()
        {
            _characterConfig = ConfigInstaller.Instance.CharactersEntity.GetCharacterConfig(_characterType);
        }

        private void CreateCharacterModel()
        {
            var model = Instantiate(_characterConfig.Prefab, transform);
            _characterSingleManager = model.GetComponent<CharacterSingleManager>();
        }

        private void SetTextureModel()
        {
            _characterSingleManager.SetRenderer(_characterConfig.Textures[_skinIndex]);
        }
    }
}