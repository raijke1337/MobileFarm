using System;
using UnityEngine;
using static Enums;
using Random = UnityEngine.Random;

namespace Farm.Game
{
    [Serializable]
    public class SoilTileState
    {

        public float TotalStateDuration { get; }
        public float RemainingDuration { get;private set; }
        public Sprite[] StateSprites { get; }
        public SoilState StateType { get; }
        private float _newPictureTime = 3f;
        private int _currentPictureIndex = 0;
        public SoilTileState (Sprite[] stateSprites, SoilState sta, float duration)
        {
            TotalStateDuration = duration;
            RemainingDuration = duration;
            StateSprites = stateSprites;
            StateType = sta;
            int newInd = Random.Range(0, StateSprites.Length - 1);
            _currentPictureIndex = newInd;
        }
        /// <summary>
        /// makes it last forever
        /// </summary>
        /// <param name="stateSprites"></param>
        /// <param name="sta"></param>
        public SoilTileState(Sprite[] stateSprites, SoilState sta)
        {
            TotalStateDuration = float.MaxValue;
            RemainingDuration = float.MaxValue;
            StateSprites = stateSprites;
            StateType = sta;
            int newInd = Random.Range(0, StateSprites.Length - 1);
            _currentPictureIndex = newInd;
        }
        public bool TestExpiry (float delta)
        {
            RemainingDuration -= delta;
            return RemainingDuration < 0;
        }
        public void ResetDuration()
        {
            RemainingDuration = TotalStateDuration;
        }
        public Sprite NewPicture(bool initial=false)
        { 
            if (initial)
            {
                return StateSprites[_currentPictureIndex];
            }

            if (_newPictureTime > 0)
            {
                _newPictureTime -= Time.deltaTime;
                return StateSprites[ _currentPictureIndex];
            }
            else
            {
                _newPictureTime = 3f;
                int newInd = Random.Range(0, StateSprites.Length - 1);
                _currentPictureIndex = newInd;
                return StateSprites[newInd];
            }

        }

    }
}