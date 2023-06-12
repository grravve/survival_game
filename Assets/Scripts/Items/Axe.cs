using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class Axe : Item
    {
        public float ExtractionSpeed { get; }

        public Axe(ItemModel model, float extractionSpeed) : base(model)
        {
            ExtractionSpeed = extractionSpeed;
        }

        public void Use()
        {

        }
    }
}
