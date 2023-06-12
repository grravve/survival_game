using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class ClimateZoneController
    {
        private static List<ClimateZone> _climateZones;

        public List<ClimateZone> GetClimateZones() => _climateZones;

        public ClimateZoneController(List<AreaVisualModel> climateAreaModels)
        {
            if(_climateZones != null)
            {
                ClearClimateZones();
            }

            _climateZones = new List<ClimateZone>();

            foreach(var model in climateAreaModels)
            {
                _climateZones.Add(new ClimateZone(model));
            }
        }

        private void ClearClimateZones()
        {
            foreach (var zone in _climateZones)
            {
                zone.ClearZoneObjects();
            }
        }
    }
}
