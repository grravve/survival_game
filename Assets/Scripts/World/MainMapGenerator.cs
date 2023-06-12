using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Scripts
{
    class MainMapGenerator: AbstractMapGenerator
    {
        [SerializeField]
        private int _areaWidth = 30, _areaHeight = 20;

        [SerializeField]
        private int _minSplitAreaWidth = 4, _minSplitAreaHeight = 3;

        [SerializeField]
        private int _limiterValue = 10;

        [SerializeField]
        private Tile _limiterTile;

        [SerializeField]
        private int _offset = 0;

        [SerializeField]
        private List<ClimateZoneModel> _climateZoneModels;

        private List<AreaVisualModel> _climateAreas = new List<AreaVisualModel>();

        public ClimateZoneController ClimateZoneController { get; private set; }

        protected override void StartProceduralGeneration()
        {
            CreateGameArea();
            // Generate climate zone controller
            ClimateZoneController = new ClimateZoneController(_climateAreas);

            // Generate start items in the world
        }

        private void CreateGameArea()
        {
            BoundsInt splitingArea = new BoundsInt((Vector3Int)_startPosition, new Vector3Int(_areaWidth, _areaHeight, 0)); 
            
            List<Vector2Int> limitingAreaTilesPositions = CreateLimitingArea(splitingArea);
            _tilemapVisualizer.PaintLimitingAreaTiles(limitingAreaTilesPositions, _limiterTile);

            var splitedAreas = BinarySpacePartitionAlgorithm.Generate(splitingArea, _minSplitAreaWidth, _minSplitAreaHeight);
           
            CreateClimateAreas(splitedAreas);

            foreach(var climateArea in _climateAreas)
            {
                _tilemapVisualizer.PaintFloorTiles(climateArea);
            }
        }

        private List<Vector2Int> CreateLimitingArea(BoundsInt area)
        {
            List<Vector2Int> tilesPositions = new List<Vector2Int>();

            BoundsInt firstLimitArea = new BoundsInt(new Vector3Int(area.min.x, area.min.y -_limiterValue, 0), new Vector3Int(area.size.x + _limiterValue, _limiterValue, 0));
            BoundsInt secondLimitArea = new BoundsInt(new Vector3Int(area.max.x, area.min.y, 0), new Vector3Int(_limiterValue, area.size.y + _limiterValue, 0));
            BoundsInt thirdLimitArea = new BoundsInt(new Vector3Int(area.min.x - _limiterValue, area.max.y, 0), new Vector3Int(area.size.x + _limiterValue, _limiterValue, 0));
            BoundsInt fourthLimitArea = new BoundsInt(new Vector3Int(area.min.x - _limiterValue, area.min.y - _limiterValue, 0), new Vector3Int(_limiterValue, area.size.y + _limiterValue, 0));

            tilesPositions.AddRange(BoundsToList(firstLimitArea));
            tilesPositions.AddRange(BoundsToList(secondLimitArea));
            tilesPositions.AddRange(BoundsToList(thirdLimitArea));
            tilesPositions.AddRange(BoundsToList(fourthLimitArea));

            return tilesPositions;
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

        private List<Vector2Int> BoundsToList(BoundsInt area)
        {
            HashSet<Vector2Int> positions = new HashSet<Vector2Int>();
            for (int column = 0; column < area.size.x; column++)
            {
                for (int row = 0; row < area.size.y; row++)
                {
                    Vector2Int position = (Vector2Int)area.min + new Vector2Int(column, row);
                    positions.Add(position);
                }
            }

            return positions.ToList();
        }

    }
}
