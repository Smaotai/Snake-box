using UnityEngine;
using UnityEngine.UI;


namespace Snake_box
{
    public abstract class BaseCharacter : MonoBehaviour
    {

        #region Fields     

        protected float _baseArmor;///Базовое кол-во Щита
        protected float _currentArmor;//Текущее кол-во Щита
        protected float _snakeArmorGeneration;//Время регенерации щита
        protected float _baseSnakeHp;///Базовое кол-во Здоровья
        protected float _currentSnakeHp;//Текущее кол-во Здоровья
        protected float _damage;//урон
        protected float _speed;//скорость
        protected float _angularSpeed;
        private Direction _direction = Direction.Up;
        protected AnimationCurve _animation;
        public GameObject ParticleOnDie;
        public Image hpbar;

        #endregion


        #region Properties

        public float SnakeSpeed { get => _speed;  set => _speed = value; }
        public float BaseSnakeHp { get => _baseSnakeHp;  set => _baseSnakeHp = value; }
        public float CurrentSnakeHp { get => _currentSnakeHp;  set => _currentSnakeHp = value; }
        public float BaseSnakeArmor { get => _baseArmor;  set => _baseArmor = value; }
        public float CurrentSnakeArmor { get => _currentArmor;  set => _currentArmor = value; }
        public float SnakeArmorGeneration { get => _snakeArmorGeneration; set => _snakeArmorGeneration = value; }
        public float Damage { get => _damage; set => _damage = value; }

        #endregion


        #region Methods

        public void SetArmor(float damage)///нанесения урона с зашитой
        {
            _currentArmor -= damage;
            if (_currentArmor < 0)// если защита отрицательная 
            {
                SetDamage(-_currentArmor); /// то урон переносится на HP
                _currentArmor = 0;
            }
        }

        public void SetDamage(float damage)///нанесения урона без зашиты
        {
            _currentSnakeHp -= damage;
            hpbar.fillAmount = (float) _currentSnakeHp/ (float) _baseSnakeHp ;
            if (_currentSnakeHp <= 0)
            {
                Die();
            }
        }

        public void Die()
        {
            FindObjectOfType<CameraForMovie>().ReserCamera();
            GameObject.Instantiate(ParticleOnDie,transform.position,Quaternion.identity);
            gameObject.SetActive(false);
            Services.Instance.LevelService.IsSnakeAlive = false;
            Services.Instance.LevelService.EndLevel();
        }

        public void Heal()
        {
            _currentSnakeHp = _baseSnakeHp;
        }

        #endregion

    }
}
