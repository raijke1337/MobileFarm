using Farm.Game.Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Farm.Game
{
    public class CameraCtrl : MonoBehaviour
    {
        private Unit _player;
        private float zOffset = -10f;


        [SerializeField] private float _smoothTime = 1f;

        private Vector3 smoothvelocity;

        private void Update()
        {
            if (_player == null)
            {
                try
                {
                    _player = GameManager.Instance.GetPlayer;
                }
                catch
                {

                }
            }
            transform.position = Vector3.SmoothDamp(transform.position, 
                new Vector3(_player.transform.position.x, _player.transform.position.y, zOffset),ref smoothvelocity, _smoothTime);
        }

    }
}