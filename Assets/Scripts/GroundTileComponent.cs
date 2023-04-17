using Farm.Game.Entities;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.EventSystems;
using static Enums;

namespace Farm.Game
{
    [RequireComponent(typeof(Collider2D),typeof(SpriteRenderer))]
    public class GroundTileComponent : MonoBehaviour
    {

        //TODO ? Maybe make this better somehow
        // probably a good idea to serialize this as a dictionary

        #region state

        [SerializeField,Space] protected SoilState _initialState;

        [SerializeField, Space] protected Sprite[] RockSprites;
        [SerializeField] protected Sprite[] WaterSprites;

        [SerializeField,Space] protected Sprite[] GrassSprites;
        [SerializeField] protected Sprite[] DrySprites;
        [SerializeField] protected Sprite[] HoedSprites;
        [SerializeField] protected Sprite[] WateredSprites;

        private Stack<SoilTileState> _states = new Stack<SoilTileState>();

        public SoilTileState GetTileState => _states.Peek();

        private void InitialStateSetup()
        {
            switch (_initialState)
            {
                case SoilState.None:
                    Debug.LogWarning($"Initial state not set for tile {this}");
                    break;
                case SoilState.Rocks:
                    _states.Push(new SoilTileState(RockSprites, _initialState));
                    break;
                case SoilState.Water:
                    _states.Push(new SoilTileState(WaterSprites, _initialState));
                    break;
                case SoilState.Grass:
                    _states.Push(new SoilTileState(GrassSprites, _initialState));
                    break;
                case SoilState.Cut:
                    _states.Push(new SoilTileState(DrySprites, _initialState));
                    break;
                case SoilState.Watered:
                    _states.Push(new SoilTileState(WateredSprites, _initialState));
                    break;
                case SoilState.Hoed:
                    _states.Push(new SoilTileState(HoedSprites, _initialState));
                    break;
            }
            _spriteRenderer.sprite = GetTileState.StateSprites[0];
        }

        #endregion

        private Plant _plant;
        public bool IsPassable { get => (_initialState!=SoilState.Rocks && _initialState != SoilState.Water && _initialState!=SoilState.None); } // todo placeholder

        protected SpriteRenderer _spriteRenderer;

        protected virtual void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            InitialStateSetup();
        }
        private void Update()
        {
            if (_states.Count == 0) return;

            _spriteRenderer.sprite = GetTileState.NewPicture();

            if (GetTileState.TestExpiry(Time.deltaTime))
            {
                _states.Pop();
            }
        }

        public bool UseTile(Item usedItem)
        {
            bool success = false;
            
            switch (usedItem.Type)
            {
                case ItemType.None:
                    Debug.LogWarning($"{usedItem} has no type set");
                    break;
                case ItemType.Sickle:
                    if (GetTileState.StateType == SoilState.Grass)
                    {
                        GetTileState.ResetDuration();
                        _states.Push(new SoilTileState(DrySprites, SoilState.Cut, 40f));
                        success = true;
                    }
                    break;
                case ItemType.Hoe:
                    if (GetTileState.StateType == SoilState.Cut)
                    {
                        GetTileState.ResetDuration();
                        _states.Push(new SoilTileState(HoedSprites, SoilState.Hoed, 20f));
                        success = true;
                    }
                    break;
                case ItemType.Pail:
                    if (GetTileState.StateType == SoilState.Hoed)
                    {
                        GetTileState.ResetDuration();
                        _states.Push(new SoilTileState(WateredSprites, SoilState.Watered, 20f));
                        success = true;
                    }
                    if (GetTileState.StateType == SoilState.Water)
                    {
                        Debug.Log($"{usedItem} recharge from water NYI");
                    }
                    break;
                case ItemType.Shovel:
                    Debug.Log($"{usedItem} harvest NYI");
                    break;
                case ItemType.Seed:
                    if (usedItem is SeedsItem s && GetTileState.StateType == SoilState.Watered && _plant == null)
                    {
                        _plant = Instantiate(s.Plant,transform);
                        _plant.Tile = this;
                        success = true;
                    }
                    break;
            }
            if (success)
            {
                _spriteRenderer.sprite = GetTileState.NewPicture(true);
            }

            Debug.Log($"Tile{this} state is {GetTileState.StateType}, time remaining {GetTileState.RemainingDuration} used {usedItem}, result is {success}");

            return success;
        }
    }
}