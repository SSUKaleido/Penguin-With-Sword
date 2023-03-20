using System.Collections.Generic;
using Data;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "OrderData", menuName = "OrderData", order = 2)]
    public class OrderData : ScriptableObject
    {
        public Sprite sprite;
        public List<ObjectData> objects;
    }
}