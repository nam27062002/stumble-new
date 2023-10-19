using Characters.Config.Character;
using UnityEngine;
namespace Characters
{
    public class CharacterManager : MonoBehaviour
    {
        
        [SerializeField] private CharactersEntity charactersEntity;
        [SerializeField] private CharacterType characterType;
        [SerializeField] private int skinIndex = 0;
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
            _characterConfig = charactersEntity.GetCharacterConfig(characterType);
        }

        private void CreateCharacterModel()
        {
            var model = Instantiate(_characterConfig.Prefab, transform);
            _characterSingleManager = model.GetComponent<CharacterSingleManager>();
        }

        private void SetTextureModel()
        {
            _characterSingleManager.SetRenderer(_characterConfig.Textures[skinIndex]);
        }
    }
}