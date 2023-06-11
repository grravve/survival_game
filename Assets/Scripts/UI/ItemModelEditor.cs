using UnityEditor;
using UnityEditor.TerrainTools;
using UnityEngine;


namespace Assets.Scripts
{
    [CustomEditor(typeof(ItemModelEditor), true)]
    public class ItemModelEditor: Editor
    {
        private ScriptableObject _itemScriptableObject;

        private void Awake()
        {
            _itemScriptableObject = (ScriptableObject)target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if(GUILayout.Button("Type"))
            {
                Debug.Log("Type pressed");
            }
        }
    }
}
