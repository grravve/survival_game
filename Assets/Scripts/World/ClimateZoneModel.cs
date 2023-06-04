using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Scripts
{
    [CreateAssetMenu(fileName = "ClimateZoneModel", menuName = "ScriptableObjects/MapGeneratorScriptableObject", order = 1)]
    public class ClimateZoneModel : ScriptableObject
    {
        public int MaxProps;
        public int MinProps;
        public int MaxNPC;
        public int MinNPC;
        public string Name;
        public List<Tile> FloorTiles;
        
        // Props
        // NPCs
    }
}
