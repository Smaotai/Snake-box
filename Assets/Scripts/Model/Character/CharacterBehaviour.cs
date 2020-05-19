﻿using System.Collections.Generic;
using UnityEngine;


namespace Snake_box
{
    public sealed class CharacterBehaviour : MonoBehaviour
    {
        #region Fields       
       
        [SerializeField] private float _radius;
        private CharacterData _characterData;
        private BlockSnakeData _blockSnakeData;
        private readonly List<BlockSnake> _blocksSnakes = new List<BlockSnake>();//блоки
        private readonly List<Vector3> _positions = new List<Vector3>();// позиции блоков 
        private float _sizeBlock;
        private float _snakeHp;
        private float _snakeArmorCurrent;
        private float _snakeArmorMax=100;
        private float _snakeArmorGeneration=1;
        private float _damage;



        #endregion


        #region Unity Method

        private void Awake()
        {   
            for (int i = 0; i < _blocksSnakes.Count; i++)// проверяем и создоем хвост если есть
            {
                AddBlock();
            }
            _sizeBlock = (gameObject.GetComponent<MeshFilter>().sharedMesh.bounds.size.sqrMagnitude);// размер
            _characterData = Data.Instance.Character;
            _positions.Add(gameObject.transform.position);//позиция головы 
            _snakeArmorCurrent = _characterData.GetArmor();
            _snakeHp = _characterData.GetHp();
            _damage = _characterData.GetDamage();
        }

        #endregion


        #region Methods

        public void ResetPosition()///выставление блока
        {
            float distance = ((Vector3)gameObject.transform.position - _positions[0]).magnitude;/// текущая текущай  поз и последней         
            if (_blocksSnakes.Count != 0)
            {
                for (int i = 0; i < _blocksSnakes.Count; i++)// перебираем блоки
                {
                    _blocksSnakes[i].transform.position = Vector3.Lerp(_positions[i + 1], _positions[i], distance / _sizeBlock);
                    _blocksSnakes[i].transform.rotation = transform.rotation;
                }
            }
            if (distance > _sizeBlock) ///проверяем дистанцию длля перемещения
            {
                // Направление от старого положения головы, к новому
                Vector3 direction = (gameObject.transform.position - _positions[0]).normalized;
                _positions.Insert(0, _positions[0] + direction * _sizeBlock);
                _positions.RemoveAt(_positions.Count - 1);
                distance -= _sizeBlock;
            }
        }        

        public void AddBlock()// добавление блока
        {
            if (_blocksSnakes.Count < 4)
            {
                _blockSnakeData = Data.Instance.BlockSnake;
                var block = _blockSnakeData.Initialization();
                block.transform.SetParent(gameObject.transform);
                block.transform.position = _positions[_positions.Count - 1];
                _blocksSnakes.Add(block);
                _positions.Add(block.transform.position);
                _snakeHp += _blockSnakeData.GetHp();
            }
        }
       

        public void Collision()
        {
            var tagCollider = Physics.OverlapSphere(transform.position, _radius);

            for (int i = 0; i < tagCollider.Length; i++)
            {
                if (tagCollider[i].CompareTag(TagManager.GetTag(TagType.Bonus)))
                {
                    Destroy(tagCollider[i].transform.gameObject);   
                }
                if (tagCollider[i].CompareTag(TagManager.GetTag(TagType.Enemy)))
                {                 

                    //if (tagCollider[i] is IDamageAddressee ida)///не работает
                    //{
                    //    Debug.Log("enemy=idamage");
                    //    ida.RegisterDamage(_damage, ArmorTypes.Bare);
                    //}
                       
                    /////враг получает урон а змейка урон для силового поля

                    ///////если соприкасается с врагом то отнимает силовое поле//нанисение урона змейки от врага Олег должен сделать при соприкосновении с блоком
                    ///как артем отслеживаает врагов в поле зрении по тегу или по типу чтобы  нанести урон врагам

                }
                if (tagCollider[i].CompareTag(TagManager.GetTag(TagType.Base)))
                {
                    
                }
                if (tagCollider[i].CompareTag(TagManager.GetTag(TagType.Wall)))
                {
                   
                }
            }
        }

        public BlockSnake GetBlock(int indexBlock)
        {          
            if (indexBlock < _blocksSnakes.Count)
            {
                return _blocksSnakes[indexBlock];
            }
            else return null;
        }

        public void Move(float inputAxis)//движение
        {           
            transform.Rotate(0,inputAxis*90,0);
            transform.position += transform.right*(_characterData.GetSpeed() / (_positions.Count + _characterData.GetSlow()));
            ResetPosition();           
        }

        public void RegenerationArmor()
        {
            if (_snakeArmorCurrent < _snakeArmorMax)
            {
                _snakeArmorCurrent = _snakeArmorCurrent + ( _snakeArmorGeneration * Services.Instance.TimeService.DeltaTime());
            }
        }

        public void SetDamage(float damage)///нанесения урона без зашиты
        {
            _snakeHp -= damage;
            if (_snakeHp <= 0)
            {
                Destroy(gameObject);
            }
        }

        public void SetArmor(float damage)///нанесения урона с зашитой
        {
            _snakeArmorCurrent -= damage; 
            if (_snakeArmorCurrent < 0)// если защита отрицательная 
            {
                SetDamage(_snakeArmorCurrent); /// то урон переносится на HP
            }
        }



        #endregion
    }
}
