using GameContent.InventorySystem.SimpleInventorySystem;
using UnityEditor;
using UnityEngine;

namespace GameContent.Editor
{
    [CustomEditor(typeof(InventoryItemData))]
    public class InventoryDataEditor : UnityEditor.Editor
    {
        private InventoryItemData _inventoryItemData;

        private void OnEnable()
        {
            _inventoryItemData = target as InventoryItemData;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (_inventoryItemData.icon == null) return;

            var texture = AssetPreview.GetAssetPreview(_inventoryItemData.icon);
            // GUILayout.Box(texture);
            GUILayout.Space(20);
            GUILayout.Label("Icon Item Preview:");
            GUILayout.Label("", GUILayout.Height(200), GUILayout.Width(200));
            GUI.DrawTexture(GUILayoutUtility.GetLastRect(), texture);
        }
    }
}