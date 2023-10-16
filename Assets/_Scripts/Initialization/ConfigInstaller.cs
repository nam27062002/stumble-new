using Characters.Config;
using Extensions;
using UnityEngine;

namespace _Scripts.Initialization
{
    public class ConfigInstaller : Singleton<ConfigInstaller>
    { 
        [SerializeField] private CharactersEntity _charactersEntity;

        public CharactersEntity CharactersEntity => _charactersEntity;
    }
}