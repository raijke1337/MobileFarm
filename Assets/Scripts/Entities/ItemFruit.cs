using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Farm.Game.Entities
{
    public class ItemFruit : Item
    {
        [SerializeField] private float _expiry;
        private float _currentExpiration;
        public float TimeToExpiration => _currentExpiration;
        protected override void Update()
        {
            _currentExpiration -= Time.deltaTime;
            if (_currentExpiration < 0)
            {
                Debug.Log($"{_itemName} expired!");
                Destroy(gameObject); // todo might cause issues in inventory
            }
        }
        protected override void Start()
        {
            base.Start();
            _currentExpiration = _expiry;
        }
    }
}