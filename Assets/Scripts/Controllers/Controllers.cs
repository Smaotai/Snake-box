namespace Snake_box
{
    public sealed class Controllers : IInitialization, ICleanUp
    {
        #region Fields
        
        private readonly IExecute[] _executeControllers;
        private readonly ICleanUp[] _cleanUps;
        private readonly IInitialization[] _initializations;

        #endregion


        #region Properties
        
        public int Length => _executeControllers.Length;

        public IExecute this[int index] => _executeControllers[index];
        
        #endregion
        

        #region ClassLifeCycles
        
        public Controllers()
        {
            _initializations = new IInitialization[1];
            _executeControllers = new IExecute[7];
            _executeControllers[0] = new TimeRemainingController();
            _executeControllers[1] = new CharacterController();
            _executeControllers[2] = new EnemySpawnController();
            _executeControllers[3] = new EnemyController();
            _executeControllers[4] = new TurretController();
            _executeControllers[5] = new TurretProjectileController();
            _executeControllers[6] = new InputController();
            _initializations[0] = new BonusSpawnController();            

            //            _initializations = new IInitialization[0];
            //            _executeControllers = new IExecute[3];
            //            _executeControllers[0] = new TimeRemainingController();
            //            _executeControllers[1] = new TurretController();
            //            _executeControllers[2] = new TurretProjectileController();


            _cleanUps = new ICleanUp[1];
            _cleanUps[0] = new TimeRemainingCleanUp();
        }
        
        #endregion


        #region IInitialization

        public void Initialization()
        {
            for (var i = 0; i < _initializations.Length; i++)
            {
                var initialization = _initializations[i];
                initialization.Initialization();
            }
            
            for (var i = 0; i < _executeControllers.Length; i++)
            {
                var execute = _executeControllers[i];
                if (execute is IInitialization initialization)
                {
                    initialization.Initialization();
                }
            }
        }
        
        #endregion
        
        
        #region ICleanUp

        public void Cleaner()
        {
            for (var index = 0; index < _cleanUps.Length; index++)
            {
                var cleanUp = _cleanUps[index];
                cleanUp.Cleaner();
            }
        }

        #endregion
    }
}
