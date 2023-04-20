using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public abstract class AbstractMapGenerator: MonoBehaviour
    {
        [SerializeField]
        protected TilemapVisualizer _tilemapVisualizer;
        [SerializeField]
        protected Vector2Int _startPosition = Vector2Int.zero;

        public void GenerateMap()
        {
            _tilemapVisualizer.Clear();
            StartProceduralGeneration();
        }

        protected abstract void StartProceduralGeneration();
    }
}
