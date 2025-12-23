using System;
using System.Collections;
using Interactables.Interfaces;
using Unity.VisualScripting;
using UnityEngine;

namespace Interactables.Weapons
{
    public abstract class Weapon : MonoBehaviour, IUseable, IEquippable
    {
        #region Members
        [SerializeField] protected WeaponData data;
        private bool onCooldown;
        private bool isEquipped;
        private Collider collider;
        private Rigidbody rigidbody;
        [SerializeField] private RaycastInteractor raycastInteractor;
        [SerializeField] protected PlayerAnimatorController animatorController;
        #endregion
        
        #region Initialisation
        protected virtual void Awake()
        {
            collider = GetComponent<Collider>();
            rigidbody = GetComponent<Rigidbody>();
            
            if (!rigidbody)
                Debug.LogError($"{name} missing Rigidbody");
            
            if (!collider)
                Debug.LogError($"{name} missing Collider");

            if (!raycastInteractor)
                Debug.LogError($"{name} missing RaycastInteractor");
            
        }

        public void Initialise(PlayerAnimatorController anim)
        {
            animatorController = anim;
        }
        
        #endregion
        
        #region IUseable
        
        public virtual void Use()
        {
            if (onCooldown || !isEquipped) return;
            StartCoroutine(UseRoutine());
        }

        protected virtual IEnumerator UseRoutine()
        {
            onCooldown = true;

            if (animatorController != null)
            {
                animatorController.PlayUse();
            }
            else
            {
                Debug.LogError($"{name} missing AnimatorController");
            }
            
            
            GameObject hitObject = raycastInteractor.PerformRaycast(data.range, data.layerMask);

            if (hitObject != null &&
                hitObject.TryGetComponent<IHealthModifier>(out var health))
            {
                health.ModifyHealth(data.damage);
            }

            yield return new WaitForSeconds(data.cooldown);
            onCooldown = false;
        }
        
        #endregion
        
        
        #region IEquippable
            
        public virtual void Equip(Transform hand)
        {
            if (data.overrideController != null)
                animatorController.ApplyOverride(data.overrideController);
            
            isEquipped = true;
            transform.SetParent(hand);
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            collider.enabled = false;
            rigidbody.isKinematic = true;
        }

        public virtual void Unequip()
        {
            animatorController.ClearOverride();
            
            isEquipped = false;
            transform.SetParent(null);
            Vector3 dropPosition = transform.position + Vector3.forward * 2f;
            transform.position = dropPosition;
            collider.enabled = true;
            rigidbody.isKinematic = false;
        }
        
        #endregion
    }
}