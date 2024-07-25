using System.Collections;
using System.Collections.Generic;
using System.IO;
using GameContent.InventorySystem.SimpleInventorySystem;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace GameContent.ScreenShotIconMaker
{
    public class ScreenShotIconMaker : MonoBehaviour
    {
        [SerializeField] private Camera _camera;

        public List<GameObject> sceneObjects;
        public List<InventoryItemData> dataObjects;
        private readonly string _pathFolder = "/GameContent/Art/UI/GeneratedIcons/";

        [ContextMenu("TakeScreenShot")]
        public void ProcessScreenShots()
        {
            StartCoroutine(ScreenShot());
        }

        private IEnumerator ScreenShot()
        {
            for (var i = 0; i < sceneObjects.Count; i++)
            {
#if UNITY_EDITOR

                var obj = sceneObjects[i];
                var data = dataObjects[i];

                obj.gameObject.SetActive(true);

                yield return null;

                TakeScreenShot($"{Application.dataPath}/{_pathFolder}/{data.id}_Icon.png");

                yield return null;

                obj.gameObject.SetActive(false);

                var s = AssetDatabase.LoadAssetAtPath<Sprite>($"Assets/{_pathFolder}/{data.id}_Icon.png");

                if (s != null)
                {
                    data.icon = s;
                    EditorUtility.SetDirty(data);
                }
#endif

                yield return null;
            }
        }

        private void TakeScreenShot(string fullPath)
        {
            if (_camera is null) _camera = GetComponent<Camera>();

            var renderTexture = new RenderTexture(256, 256, 24);
            _camera.targetTexture = renderTexture;
            var screenShot = new Texture2D(256, 256, TextureFormat.RGBA32, false);

            _camera.Render();

            RenderTexture.active = renderTexture;
            screenShot.ReadPixels(new Rect(0, 0, 256, 256), 0, 0);
            _camera.targetTexture = null;
            RenderTexture.active = null;

            if (Application.isEditor)
                DestroyImmediate(renderTexture);
            else
                Destroy(renderTexture);

            var bytes = screenShot.EncodeToPNG();
            File.WriteAllBytes(fullPath, bytes);

            Debug.Log($"File saved in {fullPath}");

#if UNITY_EDITOR
            AssetDatabase.Refresh();
#endif
        }
    }
}