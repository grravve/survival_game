using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Assets.Scripts
{
    public class ClimateZoneController
    {
        public EventHandler<ClimateZoneObjectDestroyedEventArgs> OnClimateZoneObjectDestroyed;
        public EventHandler<OnTilesChangedEventArgs> OnTilesChanged;

        public static ClimateZoneController Instance { get; private set; }
        public static List<ClimateZone> ClimateZones { get; private set; }
        
        private static Dictionary<GameObject, Guid> _climateZoneObjects;


        public ClimateZoneController(List<AreaVisualModel> climateAreaModels)
        {
            Instance = this;

            if(ClimateZones != null)
            {
                ClearClimateZones();
                _climateZoneObjects.Clear();
            }

            ClimateZones = new List<ClimateZone>();

            foreach (var model in climateAreaModels)
            {
                var newClimateZone = new ClimateZone(model);

                newClimateZone.OnTilesChanged += OnTilesChanged_SendRequestToRedraw;
                ClimateZones.Add(newClimateZone);
                
            }

            ClimateZoneObjectsInitialization();
            Debug.Log($"Climate zones: {ClimateZones.Count}");
            Debug.Log($"Climate zones objects: {_climateZoneObjects.Count}");

            OnClimateZoneObjectDestroyed += OnClimateZoneObjectDestroyed_FindClimateZone;
        }

        public void ClimateZoneObjectsInitialization()
        {
            _climateZoneObjects = new Dictionary<GameObject, Guid>();

            for(int i = 0; i < ClimateZones.Count; i++)
            {
                List<GameObject> climateZoneObjects = new List<GameObject>(ClimateZones[i].GetClimateZoneObjects());

                for(int j = 0; j < climateZoneObjects.Count; j++)
                {
                    _climateZoneObjects.Add(climateZoneObjects[j], ClimateZones[i].ZoneID);
                }
            }
        }

        private void OnClimateZoneObjectDestroyed_FindClimateZone(object sender, ClimateZoneObjectDestroyedEventArgs e)
        {
            GameObject destroyedObject = e.DestroyedObject;

            ClimateZone findedZone = GetClimateZone(destroyedObject);
            RemoveClimateZoneObject(destroyedObject);
            findedZone.StateMachine.OnClimateZoneObjectDestroyed.Invoke(this, new ClimateZoneObjectDestroyedEventArgs(destroyedObject));
        }

        private ClimateZone GetClimateZone(GameObject climateZoneObject)
        {
            Guid zoneID;

            _climateZoneObjects.TryGetValue(climateZoneObject, out zoneID);

            if (zoneID == null)
            {
                return null;
            }

            return FindClimateZoneByID(zoneID);
        }

        private ClimateZone FindClimateZoneByID(Guid id)
        {
            foreach(var climateZone in ClimateZones)
            {
                if(climateZone.ZoneID == id)
                {
                    return climateZone;
                }
            }
            return null;
        }

        private void RemoveClimateZoneObject(GameObject removedZoneObject)
        {
            foreach (var climateZone in _climateZoneObjects)
            {
                if(removedZoneObject == climateZone.Key)
                {
                    _climateZoneObjects.Remove(removedZoneObject);
                    FindClimateZoneByID(climateZone.Value)?.RemoveClimateZoneObject(removedZoneObject);

                    Debug.Log($"Object {removedZoneObject.name} removed");
                    Debug.Log($"Dic count: {_climateZoneObjects.Count}");
                    break;
                }
            }
        }

        private void ClearClimateZones()
        {
            foreach (var zone in ClimateZones)
            {
                zone.ClearZoneObjects();
            }
        }

        private void OnTilesChanged_SendRequestToRedraw(object sender, OnTilesChangedEventArgs e)
        {
            OnTilesChanged.Invoke(this, new OnTilesChangedEventArgs(e.TilesPosition, e.Tiles));
        }
    }
}
