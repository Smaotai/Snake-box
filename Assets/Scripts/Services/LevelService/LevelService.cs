using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;


namespace Snake_box
{
    public sealed class LevelService : Service
    {
        #region Fields

        public List<IEnemy> ActiveEnemies = new List<IEnemy>();
        public List<BaseBonus> ActiveBonus = new List<BaseBonus>();
        public List<BonusBullet> ActiveBonusBullet = new List<BonusBullet>();
        public List<BasePointer> ActivePoints = new List<BasePointer>();
        private readonly LevelData _levelData;
        public CharacterBehaviour CharacterBehaviour;
        public List <BlockSnake> BlockSnakes= new List< BlockSnake>();
        public MainBuild MainBuilds = new MainBuild();

        #endregion


        #region Properties

        public GameObject Target { get; private set; }
        public LevelType CurrentLevel { get; set; }
        public bool IsLevelSpawnEnded { get; set; }
        public bool IsTargetDestroed { get; set; }
        public bool IsSnakeAlive { get; set; }

        #endregion


        #region ClassLifeCycles

        public LevelService()
        {
            SceneManager.sceneLoaded += (arg0, mode) => LevelStart(); 
            _levelData = Data.Instance.LevelData;
            IsLevelSpawnEnded = false;
            IsTargetDestroed = false;
            Services.Instance.LevelLoadService.LevelLoaded += LevelStart;
        }

        #endregion


        #region Methods
        
        public void EndLevel()
        {
            SetPanelEndLevelActive(true);
            ActiveEnemies.Clear();
            Data.Instance.TurretData.ClearTurretList();          
            
        }

        private void LevelStart()
        {
            SetPanelEndLevelActive(false);
            FindGameObject();
            IsLevelSpawnEnded = false;
            IsTargetDestroed = false;
            if (GameObject.FindObjectOfType<NavMeshSurface>())
            {
                var surface = GameObject.FindObjectOfType<NavMeshSurface>();
                surface.BuildNavMesh();
            }
        }

        public void FindGameObject()
        {
            Target = GameObject.FindGameObjectWithTag(TagManager.GetTag(TagType.Target));
        }

        public void SetPanelEndLevelActive(bool isActive) => ScreenInterface.GetInstance().ScreenFactory.GetGameMenu().SetPanelEndLevelActive(isActive);

        #endregion

    }
}
