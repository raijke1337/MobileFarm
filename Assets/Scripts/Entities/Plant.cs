using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enums;

namespace Farm.Game.Entities
{
    public class Plant : MonoBehaviour
    {
        //[HideInInspector] public string Name;
        [HideInInspector] public GroundTileComponent Tile;
        private Item _fruit;

        [SerializeField] private float _health = 20f;
        [SerializeField] private Item FruitPrefab;
        [SerializeField] private float _timeToFruit = 60f;

        private void Update()
        {
            if (Tile.GetTileState.StateType != SoilState.Watered) _health -= Time.deltaTime;
            if (Tile.GetTileState.StateType == SoilState.Watered) _timeToFruit -= Time.deltaTime;

            if (_timeToFruit <= 0f)
            {
                if (_fruit != null) Destroy(_fruit.gameObject);
                _fruit = Instantiate(FruitPrefab, transform);
                _timeToFruit = 60f;
            }
        }

        public Item Harvest() => _fruit;

    }
}