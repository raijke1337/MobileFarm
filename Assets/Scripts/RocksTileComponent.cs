using UnityEngine;
using UnityEngine.EventSystems;
namespace Farm.Game
{
    public class RocksTileComponent : GroundTileComponent
    {
        [SerializeField] private Sprite[] _variants;

        protected override void Start()
        {
            base.Start();
            _spriteRenderer.sprite = _variants[Random.Range(0, _variants.Length)];
        }
    }
}