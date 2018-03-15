using UnityEngine;

namespace HoloToolkitExtensions.Utilities
{
    public class MeshBuilder : MonoBehaviour
    {
        void Start()
        {
            var meshFilters = GetComponentsInChildren<MeshFilter>();
            foreach (var meshFilter in meshFilters)
            {
                meshFilter.transform.gameObject.AddComponent(typeof(MeshCollider));
            }
        }
    }
}
