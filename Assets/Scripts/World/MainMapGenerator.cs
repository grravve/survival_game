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
        
        protected override void StartProceduralGeneration()
        {
            CreateAreas();
        }

        private void CreateAreas()
        {
            BoundsInt splitingArea = new BoundsInt((Vector3Int)_startPosition, new Vector3Int(_areaWidth, _areaHeight, 0)); 

            var splitedAreas = BinarySpacePartitionAlgorithm.Generate(splitingArea, _minSplitAreaWidth, _minSplitAreaHeight);

            HashSet<Vector2Int> floor = new HashSet<Vector2Int>();
            floor = CreateClimateAreas();
        }

        private HashSet<Vector2Int> CreateClimateAreas()
        {
            throw new NotImplementedException();
        }
    }
}
