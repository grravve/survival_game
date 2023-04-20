using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    class MainMapGenerator: AbstractMapGenerator
    {
        [SerializeField]
        private int _areaWidth = 30, _areaHeight = 20;

        [SerializeField]
        private int _minSplitAreaWidth = 4, _minSplitAreaHeight = 3;

        [SerializeField]
        private int _offset = 0;

        [SerializeField]
        private List<ClimateZoneModel> _climateZoneModels;

        private List<AreaVisualModel> _climateAreas = new List<AreaVisualModel>();

        protected override void StartProceduralGeneration()
        {
            CreateAreas();
        }

        private void CreateAreas()
        {
            BoundsInt splitingArea = new BoundsInt((Vector3Int)_startPosition, new Vector3Int(_areaWidth, _areaHeight, 0)); 

            var splitedAreas = BinarySpacePartitionAlgorithm.Generate(splitingArea, _minSplitAreaWidth, _minSplitAreaHeight);
            
            CreateClimateAreas(splitedAreas);

            foreach(var climateArea in _climateAreas)
            {
                _tilemapVisualizer.PaintFloorTiles(climateArea);
            }
        }

        private void CreateClimateAreas(List<BoundsInt> areaList)
        {
            HashSet<Vector2Int> floor = new HashSet<Vector2Int>();
            _climateAreas.Clear();

            foreach (var area in areaList)
            {
                for(int column = _offset; column < area.size.x - _offset; column++)
                {
                    for (int row = _offset; row < area.size.y - _offset; row++)
                    {
                        Vector2Int position = (Vector2Int)area.min + new Vector2Int(column, row);
                        floor.Add(position);
                    }
                }

                int randomIndex = UnityEngine.Random.Range(0, _climateZoneModels.Count);

                _climateAreas.Add(new AreaVisualModel(_climateZoneModels[randomIndex], floor));
                floor.Clear();
            }
        }
    }
}
