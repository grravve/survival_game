using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class ClimateZoneStateChangedEventArgs : EventArgs
    {
        public string NewModel;

        public ClimateZoneStateChangedEventArgs(string newModel)
        {
            NewModel = newModel;
        }
    }

    public class ClimateZoneObjectDestroyedEventArgs : EventArgs
    {
        public GameObject DestroyedObject;

        public ClimateZoneObjectDestroyedEventArgs(GameObject destroyedObject)
        {
            DestroyedObject = destroyedObject;
        }
    }

    public class ClimateZoneStateMachine : StateMachine
    {
        public EventHandler<ClimateZoneStateChangedEventArgs> OnClimateZoneStateChanged;      
        public EventHandler<ClimateZoneObjectDestroyedEventArgs> OnClimateZoneObjectDestroyed;
        
        public TaigaState TaigaState { get; private set; }
        public ForestState ForestState { get; private set; }
        public WastelandState WastelandState { get; private set; }
        public DesertState DesertState { get; private set; }
       
        public ClimateZoneModel CurrentModel { get; private set; }
        
        public int CurrentExtractableObjects { get; private set; }
        public int CurrentNPCs { get; private set; }

        private Dictionary<IState, string> _climateModels;

        
        public ClimateZoneStateMachine(ClimateZoneModel currentModel, int extractableObjectCount, int npcCount)
        {
            CurrentModel = currentModel;
            CurrentExtractableObjects = extractableObjectCount;
            CurrentNPCs = npcCount;

            StatesInitialization();
        }

        public override void ChangeState(IState newState)
        {
            string zoneModel = string.Empty;
            
            _climateModels.TryGetValue(newState, out zoneModel);
            OnClimateZoneStateChanged?.Invoke(this, new ClimateZoneStateChangedEventArgs(zoneModel));
            
            _currentState = newState;
            _currentState.EnterToState(this);
        }

        public override void Launch(IState initialState)
        {
            _currentState = initialState;
            _currentState.EnterToState(this);
        }

        public IState GetState(string name)
        {
            foreach(var state in _climateModels)
            {
                if(state.Value == name)
                {
                    return state.Key;
                }
            }

            return new ForestState();
        }

        public void SetClimateModel(ClimateZoneModel model)
        {
            CurrentModel = model;
        }

        protected override void StatesInitialization()
        {
            _climateModels = new Dictionary<IState, string>();

            TaigaState = new TaigaState();
            ForestState = new ForestState();
            WastelandState = new WastelandState();
            DesertState = new DesertState();

            _climateModels.Add(TaigaState, "Taiga");
            _climateModels.Add(ForestState, "Forest");
            _climateModels.Add(WastelandState, "Wasteland");
            _climateModels.Add(DesertState, "Desert");
        }
    }
}
