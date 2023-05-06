using UnityEngine;

namespace Assets.Scripts
{
    [CreateAssetMenu(fileName = "ItemModel", menuName = "ScriptableObjects/ItemScriptableObject", order = 2)]
    public class ItemModel: ScriptableObject
    {
        public string Name;
        public int MaxInSlot;
        public Sprite ItemSprite;
        public ItemType Type;
    }
}
