using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts
{
    public class ClimateZone
    {
        private string _zoneID;
        
        private AreaVisualModel _areaModel;
        private ClimateZoneStateMachine _stateMachine;

        private List<GameObject> _extractableObjects;
        private List<GameObject> _npcs;
        private List<Vector2Int> _climateZonePosition;
        
        private readonly ClimateZoneModel[] _climateZoneModels;

        public ClimateZone(AreaVisualModel model)
        {
            _zoneID = Guid.NewGuid().ToString();
            _climateZoneModels = Resources.LoadAll<ClimateZoneModel>("Data/ClimateZones");

            _areaModel = model;
            _extractableObjects = _areaModel.ClimateZoneModel.ExtractableObjects;
            _npcs = _areaModel.ClimateZoneModel.NPC;
            _climateZonePosition = _areaModel.FloorTilePositions.ToList();

            Initialization();

            _stateMachine = new ClimateZoneStateMachine();
            _stateMachine.OnClimateZoneStateChanged += OnClimateZoneStateChanged_ChangeClimateModel;

            IState initialState = _stateMachine.GetState(_areaModel.ClimateZoneModel.Name);
            _stateMachine.Launch(initialState);
        }

        ~ClimateZone()
        {
            ClearZoneObjects();
        }

        public void ClearZoneObjects()
        {
            for(int i = 0; i < _extractableObjects.Count; i++)
            {
                UnityEngine.Object.Destroy(_extractableObjects[i]);
            }
        }

        private void OnClimateZoneStateChanged_ChangeClimateModel(object sender, ClimateZoneStateMachine.ClimateZoneStateChangedEventArgs e)
        {
            ClimateZoneModel newZoneModel = FindClimateZoneModel(e.NewModel);
            SetClimateZoneModel(newZoneModel);
            // Re-generate zone
        }

        private ClimateZoneModel FindClimateZoneModel(string name)
        {
            foreach(var climateZone in _climateZoneModels)
            {
                if(climateZone.Name == name)
                {
                    return climateZone;
                }
            }

            return null;
        }

        private void SetClimateZoneModel(ClimateZoneModel climateZoneModel)
        {
            if (climateZoneModel == null)
            {
                return;
            }

            _areaModel.ChangeClimateZoneModel(climateZoneModel);
        }

        public AreaVisualModel GetClimateAreaModel()
        {
            return _areaModel;
        }

        private void Initialization()
        {
            SpawnZoneObjects();
        }

        private void SpawnZoneObjects()
        {
            // Spawn extractable objects
            _extractableObjects = ObjectSpawner.Instance.SpawnProps(_climateZonePosition, _extractableObjects, _areaModel.ClimateZoneModel.MinProps, _areaModel.ClimateZoneModel.MaxProps);
            // Spawn NPCs
        }
    }
}
