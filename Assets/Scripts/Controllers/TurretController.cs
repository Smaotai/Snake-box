﻿using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Services.InputService;
using UnityEngine;


namespace Snake_box
{
    public sealed class TurretController : IExecute, IInitialization
    {
        #region Fields

        private static TurretController _hiddenInstance;
        private TurretInitializer _newTurret;
        private List<TurretBaseAbs> _turretList = new List<TurretBaseAbs>();

        #endregion


        #region IInitializaion

        public void Initialization()
        {
            _hiddenInstance = this;
        }

        #endregion


        #region Execute

        public void Execute()
        {
            SetTurretList();

            _turretList = _turretList.Where(x => x != null ? true : false).ToList();

            _turretList.ForEach(iExecutable => iExecutable.Execute());

            if (new InputService().IsKeysPressed())
            {
                Data.Instance.TurretData.TurretPlant.ChangeTurretType(new InputService().KeyDownIs());
            }
        }

        #endregion


        #region Methods

        private void SetTurretList() => _turretList = Data.Instance.TurretData.GetTurretList();

        #endregion
    }
}