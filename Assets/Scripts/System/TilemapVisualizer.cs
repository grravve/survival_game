using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Scripts
{
    public class TilemapVisualizer: MonoBehaviour 
    {
        [SerializeField]
        private Tilemap _floorTilemap;

        [SerializeField]
        private Tilemap _wallsTilemap;

        public void PaintFloorTiles(AreaVisualModel areaVisualModel)
        {
            PaintTiles(areaVisualModel.FloorTilePositions, _floorTilemap, areaVisualModel.ClimateZoneModel.FloorTiles);
        }

        public void PaintLimitingAreaTiles(IEnumerable<Vector2Int> limitAreaTilesPositions, Tile limiterTile)
        {
            PaintTiles(limitAreaTilesPositions, _wallsTilemap, limiterTile);
        }

        public void Clear()
        {
            _floorTilemap.ClearAllTiles();
            _wallsTilemap.ClearAllTiles();
        }

        private void PaintTiles(IEnumerable<Vector2Int> tilesPositions, Tilemap tilemap, List<Tile> tiles)
        {
            foreach(var tilePosition in tilesPositions)
            {
                PaintSingleTile(tilemap, tiles[Random.Range(0, tiles.Count)], tilePosition);
            }
        }

        private void PaintTiles(IEnumerable<Vector2Int> tilesPositions, Tilemap tilemap, Tile tile)
        {
            foreach (var tilePosition in tilesPositions)
            {
                PaintSingleTile(tilemap, tile, tilePosition);
            }
        }

        private void PaintSingleTile(Tilemap tilemap, Tile tile, Vector2Int tilePosition)
        {
            var position = tilemap.WorldToCell((Vector3Int)tilePosition);
            tilemap.SetTile(position, tile);
        }
    }
}
