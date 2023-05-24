using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class AreaVisualModel
    {
        public ClimateZoneModel ClimateZoneModel { get; private set; }
        public HashSet<Vector2Int> FloorTilePositions { get; private set; }

        public AreaVisualModel(ClimateZoneModel climateZoneModel, HashSet<Vector2Int> floorTilePositions)
        {
            ClimateZoneModel = climateZoneModel;
            FloorTilePositions = new HashSet<Vector2Int>(floorTilePositions);
        }

        public void ChangeClimateZoneModel(ClimateZoneModel newClimateZoneModel)
        {
            ClimateZoneModel = newClimateZoneModel;
        }
    }
}
