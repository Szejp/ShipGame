using Interfaces;
using LevelManagement;
using QFramework.GameModule.GameTools.ObjectPool;
using QFramework.Helpers;
using QFramework.Helpers.Spawning;
using UnityEngine;

namespace Controllers
{
    public class GameMaster : Singleton<GameMaster>
    {
        [SerializeField] private Player _player;

        [SerializeField] LevelManagerConfig levelManagerConfig;

        private IMovementController movementController;
        private IScoreController scoreController;
        private ISpawner spawner;

        public ISpawner Spawner
        {
            get
            {
                if (spawner == null)
                    spawner = new Spawner();
                return spawner;
            }
        }

        public IScoreController ScoreController
        {
            get
            {
                if (scoreController == null)
                    scoreController = new ScoreController();
                return scoreController;
            }
        }

        public IMovementController MovementController
        {
            get { return movementController; }
        }

        public Player player
        {
            get { return _player; }
        }

        protected override void Awake()
        {
            base.Awake();
            movementController = new MovementController(player.plane);
        }
    }
}