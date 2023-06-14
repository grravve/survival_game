using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Scripts
{
    public class OnTilesChangedEventArgs : EventArgs
    {
        public List<Vector2Int> TilesPosition;
        public List<Tile> Tiles;

        public OnTilesChangedEventArgs(List<Vector2Int> tilesPosition, List<Tile> tiles)
        {
            TilesPosition = new List<Vector2Int>(tilesPosition);
            Tiles = new List<Tile>(tiles);
        }
    }

    public class ClimateZone
    {
        public Guid ZoneID { get; private set; }
        public AreaVisualModel CurrentAreaModel { get; private set; }
        public EventHandler<OnTilesChangedEventArgs> OnTilesChanged;

        public ClimateZoneStateMachine StateMachine { get; private set; }

        private List<GameObject> _extractableObjectsSpawned;
        private List<GameObject> _npcsSpawned;
        private List<Vector2Int> _climateZonePosition;
        
        private readonly ClimateZoneModel[] _climateZoneModels;


        public ClimateZone(AreaVisualModel model)
        {
            ZoneID = Guid.NewGuid();
            _climateZoneModels = Resources.LoadAll<ClimateZoneModel>("Data/ClimateZones");

            CurrentAreaModel = model;
            _extractableObjectsSpawned = CurrentAreaModel.ClimateZoneModel.ExtractableObjects;
            _npcsSpawned = CurrentAreaModel.ClimateZoneModel.NPC;
            _climateZonePosition = CurrentAreaModel.FloorTilePositions.ToList();

            int npcCount = UnityEngine.Random.Range(CurrentAreaModel.ClimateZoneModel.MinNPC, CurrentAreaModel.ClimateZoneModel.MaxNPC);
            int extractableObjectCount = UnityEngine.Random.Range(CurrentAreaModel.ClimateZoneModel.MinProps, CurrentAreaModel.ClimateZoneModel.MaxProps);

            SpawnZoneObjects(npcCount, extractableObjectCount);

            StateMachine = new ClimateZoneStateMachine(model.ClimateZoneModel, extractableObjectCount, npcCount);
            StateMachine.OnClimateZoneStateChanged += OnClimateZoneStateChanged_ChangeClimateModel;

            IState initialState = StateMachine.GetState(CurrentAreaModel.ClimateZoneModel.Name);
            StateMachine.Launch(initialState);
        }

        ~ClimateZone()
        {
            ClearZoneObjects();
        }

        public List<GameObject> GetClimateZoneObjects()
        {
            List<GameObject> zoneObjects = new List<GameObject>();

            zoneObjects.AddRange(_extractableObjectsSpawned);
            zoneObjects.AddRange(_npcsSpawned);

            return zoneObjects;
        }
       
        public void ClearZoneObjects()
        {
            for(int i = 0; i < _extractableObjectsSpawned.Count; i++)
            {
                UnityEngine.Object.Destroy(_extractableObjectsSpawned[i]);
            }

            for (int i = 0; i < _npcsSpawned.Count; i++)
            {
                UnityEngine.Object.Destroy(_npcsSpawned[i]);
            }

            _extractableObjectsSpawned.Clear();
            _npcsSpawned.Clear();
        }

        public void RemoveClimateZoneObject(GameObject deletingObject)
        {
            if(_extractableObjectsSpawned.Contains(deletingObject))
            {
                _extractableObjectsSpawned.Remove(deletingObject);
                
                return;
            }

            if (_npcsSpawned.Contains(deletingObject))
            {
                _npcsSpawned.Remove(deletingObject);

                return;
            }
        }
                
        private void OnClimateZoneStateChanged_ChangeClimateModel(object sender, ClimateZoneStateChangedEventArgs e)
        {
            ClimateZoneModel newZoneModel = FindClimateZoneModel(e.NewModel);
            SetClimateZoneModel(newZoneModel);
            StateMachine.SetClimateModel(newZoneModel);
            // Re-generate zone
            int currentExtractableObjectsCount = StateMachine.CurrentExtractableObjects;
            int currentNPC = StateMachine.CurrentNPCs;

            OnTilesChanged.Invoke(this, new OnTilesChangedEventArgs(CurrentAreaModel.FloorTilePositions.ToList(), CurrentAreaModel.ClimateZoneModel.FloorTiles));
            
            ClearZoneObjects();
            SpawnZoneObjects(currentNPC, currentExtractableObjectsCount);
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

            CurrentAreaModel.ChangeClimateZoneModel(climateZoneModel);
        }

        private void SpawnZoneObjects(int npcCount, int extractableObjectCount)
        {
            _extractableObjectsSpawned = ObjectSpawner.Instance.SpawnProps(_climateZonePosition, _extractableObjectsSpawned, extractableObjectCount);
            // Spawn NPCs
        }
    }
}
