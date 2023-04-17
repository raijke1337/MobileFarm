using Farm.Game.Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Farm.Game
{
    public class SeedsItem : Item
    {
        public Plant Plant { get => _plant; }
        [SerializeField] private Plant _plant;
    }
}