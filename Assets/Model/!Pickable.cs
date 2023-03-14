using UnityEngine;

namespace Penguinsword.Model
{
    public interface IPickable
    {
        GameObject GameObject { get; }
        public void Pick();
        public void Drop();
    }
}