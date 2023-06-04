using UnityEngine;

namespace Assets.Scripts
{
    public abstract class AbstractMapGenerator: MonoBehaviour
    {
        [SerializeField]
        protected TilemapVisualizer _tilemapVisualizer;
        [SerializeField]
        protected Vector2Int _startPosition = Vector2Int.zero;

        /*protected void Awake()
        {
            GenerateMap();
        }*/

        public void GenerateMap()
        {
            _tilemapVisualizer.Clear();
            StartProceduralGeneration();
        }

        protected abstract void StartProceduralGeneration();
    }
}
