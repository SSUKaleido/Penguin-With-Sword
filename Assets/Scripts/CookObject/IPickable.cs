using UnityEngine;

namespace CookObject
{
    public interface IPickable
    {
        GameObject GameObject { get; }
        public void Pick();
        public void Drop();
    }
}