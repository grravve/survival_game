﻿using UnityEngine;

namespace Assets.Scripts
{
    public class ForestState : IState
    {
        private int _maximumAnimals;
        private int _minimumAnimals;
        private int _maximumExtractableObjects;
        private int _minimumExtractableObjects;

        private int _currentAnimals;
        private int _currentExtractableObjects;

        private ClimateZoneStateMachine _context;

        public void EnterToState(StateMachine context)
        {
            _context = context as ClimateZoneStateMachine;

            if(context == null)
            {
                Debug.LogError("Initial state error - Not climate zone state machine as param");
                return;
            }

            _context.OnClimateZoneObjectDestroyed += StatementCheck;

            ClimateZoneModel currentModel = _context.CurrentModel;

            _minimumAnimals =  currentModel.MinNPC;
            _maximumAnimals = currentModel.MaxNPC;
            _minimumExtractableObjects = currentModel.MinProps;
            _maximumExtractableObjects = currentModel.MaxProps;

            _currentExtractableObjects = _context.CurrentExtractableObjects;
            _currentAnimals = _context.CurrentNPCs;
        }

        private void StatementCheck(object sender, ClimateZoneObjectDestroyedEventArgs e)
        {
            GameObject destroyedObject = e.DestroyedObject;
            
            if(destroyedObject.GetComponent<IExtractable>() != null)
            {
                _currentExtractableObjects--;
                Debug.Log($"Forest state (after extract) Current eObjects:{_currentExtractableObjects}");
            }

            // if(destroyedObject.GetComponent<Animal>() != null)
            //{
            //  _curentAnimals--;
            //}

            /*if(_currentAnimals > _maximumAnimals && _currentExtractableObjects > _maximumExtractableObjects)
            {
                _context.ChangeState(_context.TaigaState);
            }*/

            if(_currentExtractableObjects < _minimumExtractableObjects)
            {
                _context.ChangeState(_context.DesertState);
                Debug.Log("State cahnged to Desert state");
            }
        }
    }
}
