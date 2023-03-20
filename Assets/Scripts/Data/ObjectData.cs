using CookObject;
using Data;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "ObjectData", menuName = "ObjectData", order = 0)]
    public class ObjectData : ScriptableObject
    {
        public ObjectType type;
        public float processTime = 7.4f;
        public float cookTime = 6f;
        
        [Header("Visuals")]
        public Mesh rawMesh;
        public Mesh processedMesh;
        public Mesh cookedMesh;
        public Mesh overcookedMesh;
        [Tooltip("We are using the same material for raw and processed meshes")]
        public Material ingredientMaterial;
        [Tooltip("Used to tint related objects, like the Plate")]
        public Color baseColor;
        [Tooltip("UI usage")]
        public Sprite sprite;
    }
}