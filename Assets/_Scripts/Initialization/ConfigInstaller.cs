using Characters.Config.Character;
using Characters.Config.Movement;
using Extensions;
using UnityEngine;

namespace _Scripts.Initialization
{
    public class ConfigInstaller : Singleton<ConfigInstaller>
    { 
        [SerializeField] private CharactersEntity _charactersEntity;
        [SerializeField] private MovementConfig _movementConfig;

        public CharactersEntity CharactersEntity => _charactersEntity;
        public MovementConfig MovementConfig => _movementConfig;
    }
}