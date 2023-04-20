using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Scripts
{
    public class TilemapVisualizer: MonoBehaviour 
    {
        [SerializeField]
        private Tilemap _floorTilemap;

        public void PaintFloorTiles(AreaVisualModel areaVisualModel)
        {
            PaintTiles(areaVisualModel.FloorTilePositions, _floorTilemap, areaVisualModel.ClimateZoneModel.FloorTiles);
        }

        public void Clear()
        {
            _floorTilemap.ClearAllTiles();
        }

        private void PaintTiles(IEnumerable<Vector2Int> tilesPositions, Tilemap tilemap, List<Tile> tiles)
        {
            foreach(var tilePosition in tilesPositions)
            {
                PaintSingleTile(tilemap, tiles[Random.Range(0, tiles.Count)], tilePosition);
            }
        }

        private void PaintSingleTile(Tilemap tilemap, Tile tile, Vector2Int tilePosition)
        {
            var position = tilemap.WorldToCell((Vector3Int)tilePosition);
            tilemap.SetTile(position, tile);
        }
    }
}
