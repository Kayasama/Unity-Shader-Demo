using System;
using UnityEditor;
using UnityEngine;

namespace CaryChauRP.Editor.Tools
{
    public class MeshTool
    {
        #region fields
        [MenuItem("CaryChauRP/Extract Mesh To Asset")]
        public static void ExtractMeshToAsset()
        {
            GameObject gameObject = Selection.activeGameObject;
            MeshFilter meshFilter = gameObject.GetComponent<MeshFilter>();

            if(meshFilter == null)
            {
                Debug.LogError("no meshFilter");
                return;
            }

            AssetDatabase.CreateAsset(UnityEngine.Object.Instantiate<Mesh>(meshFilter.sharedMesh), "Assets/Temp/mesh_" + DateTime.Now.Millisecond + ".mesh");

        }

        [MenuItem("CaryChauRP/Create Cube")]
        public static void CreatePrimitiveCube()
        {
            SavePrimitiveMeshToAsset(PrimitiveType.Cube);
        }

        [MenuItem("CaryChauRP/Create Sphere")]
        public static void CreatePrimitiveSphere()
        {
            SavePrimitiveMeshToAsset(PrimitiveType.Sphere);
        }

        private static void SavePrimitiveMeshToAsset(PrimitiveType primitiveType)
        {
            string path = EditorUtility.SaveFilePanelInProject("select save path", "mesh_", "mesh", "select the save path");
            if (string.IsNullOrEmpty(path))
                return;
            SavePrimitiveMeshToAsset(primitiveType, path);
        }

        private static void SavePrimitiveMeshToAsset(PrimitiveType primitiveType, string path)
        {
            GameObject gameObject = GameObject.CreatePrimitive(primitiveType);
            Mesh mesh = gameObject.GetComponent<MeshFilter>().sharedMesh;
            mesh = UnityEngine.Object.Instantiate(mesh);
            UnityEngine.Object.DestroyImmediate(gameObject);
            AssetDatabase.CreateAsset(mesh, path);
        }
        #endregion

    }

}
