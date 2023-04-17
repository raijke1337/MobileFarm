using Farm.Game.Component;
using Farm.Game.Inputs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.HID;
using static Enums;
namespace Farm.Game.Entities
{
    [RequireComponent(typeof(MoveComp),typeof(PlayerInputs),typeof(InventoryComp))]
    [RequireComponent(typeof(SpriteRenderer))]
    public class Unit : MonoBehaviour
    {
        [SerializeField] public UnitType UnitType;
        public InventoryComp GetInventory
        {
            get
            {
                if (_inventoryComp == null) _inventoryComp = GetComponent<InventoryComp>();
                return _inventoryComp;
            }
        }

        private MoveComp _move;
        private PlayerInputs _playerInputs;
        private InventoryComp _inventoryComp;
        private SpriteRenderer _spriteRenderer;

        private void Start()
        {
            _playerInputs = GetComponent<PlayerInputs>();
            _spriteRenderer = GetComponent<SpriteRenderer>(); 
            _move = GetComponent<MoveComp>();


            GameManager.Instance.RegisterUnit(this);
            _playerInputs.ClickEvent += _playerInputs_ClickEvent;
            _playerInputs.MoveEvent += _playerInputs_MoveEvent;
        }

        private void OnDisable()
        {
            _playerInputs.ClickEvent -= _playerInputs_ClickEvent;
            _playerInputs.MoveEvent -= _playerInputs_MoveEvent;
        }

        private GroundTileComponent DoControlRaycast()
        {
            GroundTileComponent result = null;

            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            if (hit)
            {
                if (hit.collider.GetComponent<GroundTileComponent>())
                {
                    result = hit.collider.GetComponent<GroundTileComponent>();
                }
            }

            return result;
        }

        private void _playerInputs_MoveEvent()
        {
            var c = DoControlRaycast();
            
            if (c.IsPassable)
            {
                if (_move.Move(c.transform.position)) CheckFlip(c.transform.position);
            }
        }


        private void _playerInputs_ClickEvent()
        {
            var c = DoControlRaycast();
            if (Vector2.Distance(transform.position, c.transform.position) < _playerInputs.InteractRange)
            {
                
                if (_inventoryComp.TryUseItem()) 
                { 
                    c.UseTile(GetInventory.EquippedItem);
                    CheckFlip(c.transform.position); 
                } 
            }
        }


        private void CheckFlip(Vector3 point)
        {
            bool isFlip;
            isFlip = point.x < transform.position.x;
            _spriteRenderer.flipX = isFlip;
            _inventoryComp.Flip(isFlip);    

        }

    }
}