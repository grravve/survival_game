using Mono.Cecil.Rocks;
using System;
using System.Collections.Generic;
using UnityEngine.UIElements;

namespace Assets.Scripts
{
    public class ClimateZoneStateMachine : StateMachine
    {
        private Dictionary<IState, string> _climateModels;

        public TaigaState TaigaState { get; private set; }
        public ForestState ForestState { get; private set; }
        public WastelandState WastelandState { get; private set; }
        public DesertState DesertState { get; private set; }

        public EventHandler<ClimateZoneStateChangedEventArgs> OnClimateZoneStateChanged;
        public class ClimateZoneStateChangedEventArgs : EventArgs
        {
            public string NewModel;

            public ClimateZoneStateChangedEventArgs(string newModel)
            {
                NewModel = newModel;
            }
        }

        public ClimateZoneStateMachine()
        {
            StatesInitialization();
        }

        public override void ChangeState(IState newState)
        {
            string zoneModel = String.Empty;
            
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
