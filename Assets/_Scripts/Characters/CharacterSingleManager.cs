using UnityEngine;

namespace Characters
{
    public class CharacterSingleManager : MonoBehaviour
    {
        [SerializeField] private Renderer _renderer;

        public void SetRenderer(Texture texture)
        {
            if (_renderer != null)
            {
                _renderer.material.mainTexture = texture;
            }
            else
            {
                Debug.LogError("Renderer is not assigned.");
            }
        }
    }
}