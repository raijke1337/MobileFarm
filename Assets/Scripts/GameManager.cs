using Farm.Game;
using Farm.Game.Entities;
using Farm.Game.UI;
using System.Collections.Generic;
using UnityEngine;
using static Enums;

namespace Farm
{
    public class GameManager : MonoBehaviour
    {
        #region singleton
        public static GameManager Instance { get; private set; }
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        #endregion

        [SerializeField] private Unit _playerPrefab;
        private Unit _player;

        private List<Unit> _units = new List<Unit>();
        public GameState CurrentState;

        [Space,SerializeField] private GameCanvasController _canvasPrefab;
        [SerializeField] private Transform _gameGrid; // MASSIVE TODO



        private GameCanvasController _canvas;

        public Unit GetPlayer { get { return _player; } }  

        public void RegisterUnit(Unit unit) => _units.Add(unit);    

        private void Start()
        {
            if (CurrentState == GameState.Gameplay)
            {

                _player = Instantiate(_playerPrefab,_gameGrid);

                _canvas = Instantiate(_canvasPrefab);
                _canvas.Init();
            }
        }

        public delegate void SimpleEvent<T1, T2>(T1 arg, T2 arg2);
        public delegate void SimpleEvent();
        public delegate void SimpleEvent<T1>(T1 arg);
    }
}