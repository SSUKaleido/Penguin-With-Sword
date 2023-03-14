using System.Collections.Generic;
using UnityEngine;

namespace Penguinsword.Data
{
    [CreateAssetMenu(fileName = "OrderData", menuName = "OrderData", order = 2)]
    public class OrderData : ScriptableObject
    {
        public Sprite sprite;
        public List<ObjectData> objects;
    }
}