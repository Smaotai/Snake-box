﻿using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


namespace Snake_box
{
    public sealed class GameMenuBehaviour : BaseUi
    {
        #region Fields
        
        [SerializeField] private GameObject _endGamePanel;
        [SerializeField] private Button _buttonAddBlocks;
        [SerializeField] private Button _buttonAddTurel1;
        [SerializeField] private Button _buttonAddTurel2;
        [SerializeField] private Button _buttonAddTurel3;
        [SerializeField] private Button _buttonAddTurel4;
        [SerializeField] private Button _pause;
        [SerializeField] private Button _mainMenu;
        [SerializeField] private Button _reset;
        [SerializeField] private Text _textEndGame;
        private bool _isPause;

        #endregion
        private void Update()///для теста
        {
            if (Services.Instance.LevelService.ActiveEnemies.Count<=0)
            {
                _endGamePanel.SetActive(true);
                Services.Instance.TimeService.SetTimeScale(0);
                _textEndGame.text = "Congratulations!";
            }           
            if (TagManager.GetTag(TagType.Target) == null)
            {
                _endGamePanel.SetActive(true);
                Services.Instance.TimeService.SetTimeScale(0);
                _textEndGame.text = "Congratulations!You Loser!";

            }
            //else _endGamePanel.SetActive(false);
        }

        #region UnityMethods

        private void OnEnable()
        {
            _buttonAddBlocks.onClick.AddListener(AddBlock);
            _buttonAddTurel1.onClick.AddListener(delegate { AddTurrel(0);} );
            _buttonAddTurel2.onClick.AddListener(delegate { AddTurrel(1);} );
            _buttonAddTurel3.onClick.AddListener(delegate { AddTurrel(2);} );
            _buttonAddTurel4.onClick.AddListener(delegate { AddTurrel(3);} );
            _mainMenu.onClick.AddListener(delegate { SceneManager.LoadScene(0); });
            _reset.onClick.AddListener(delegate { SceneManager.LoadScene(1); });
            _pause.onClick.AddListener(Pause);
        }

        private void OnDisable()
        {
            _buttonAddBlocks.onClick.RemoveListener(AddBlock);
            _buttonAddTurel1.onClick.RemoveListener(delegate { AddTurrel(0); });
            _buttonAddTurel2.onClick.RemoveListener(delegate { AddTurrel(1); });
            _buttonAddTurel3.onClick.RemoveListener(delegate { AddTurrel(2); });
            _buttonAddTurel4.onClick.RemoveListener(delegate { AddTurrel(3); });
            _mainMenu.onClick.RemoveListener(delegate { SceneManager.LoadScene(0); });
            _reset.onClick.RemoveListener(delegate { SceneManager.LoadScene(0); });
            _pause.onClick.RemoveListener(Pause);
        }

        #endregion


        #region Methods

        private void AddBlock()
        {
           var _characterData = Data.Instance.Character;
            _characterData.CharacterBehaviour.AddBlock();
        }

        private void Pause()
        {
            if (!_isPause)
            {               
                Services.Instance.TimeService.SetTimeScale(0);
                _isPause = !_isPause;
            }
            else
            {
                _isPause = !_isPause;
                Services.Instance.TimeService.SetTimeScale(1);
            }
        }

        public override void Show()
        {
            gameObject.SetActive(true);
            ShowUI.Invoke();
        }

        public override void Hide()
        {
            gameObject.SetActive(false);
            HideUI.Invoke();
        }

        private void AddTurrel(int numberButton)
        {
            var _characterData = Data.Instance.Character;
            if (_characterData.CharacterBehaviour.GetBlock(numberButton))
            {               
                _characterData.CharacterBehaviour.GetBlock(numberButton).AddTurret();
            }
            
        }

        #endregion
    }
}
