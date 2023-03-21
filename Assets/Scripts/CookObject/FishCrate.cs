﻿using CookObject;
using UnityEngine;
using UnityEngine.Assertions;

namespace CookObject
{
    public class FishCrate : Interactable
    {
        [SerializeField] private Fish ingredientPrefab;
        private Animator _animator;
        private static readonly int OpenHash = Animator.StringToHash("Open");

        protected override void Awake()
        {
            base.Awake();
            _animator = GetComponentInChildren<Animator>();
            
#if UNITY_EDITOR
            Assert.IsNotNull(ingredientPrefab);
            Assert.IsNotNull(_animator);
#endif
        }

        public override bool TryToDropIntoSlot(IPickable pickableToDrop)
        {
            if (CurrentPickable != null) return false;
            
            CurrentPickable = pickableToDrop;
            CurrentPickable.GameObject.transform.SetParent(Slot);
            pickableToDrop.GameObject.transform.SetPositionAndRotation(Slot.position, Quaternion.identity);
            return true;
        }

        public override IPickable TryToPickUpFromSlot(IPickable playerHoldPickable)
        {
            if (CurrentPickable == null)
            {
                _animator.SetTrigger(OpenHash);
                //return Instantiate(ingredientPrefab, Slot.transform.position, Quaternion.identity);
            }

            var output = CurrentPickable;
            var interactable = CurrentPickable as Interactable;
            interactable?.ToggleHighlightOff();
            CurrentPickable = null;
            return output;
        }
    }
}