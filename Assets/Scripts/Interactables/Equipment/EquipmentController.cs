using System;
using Interactables.Interfaces;
using UnityEngine;

public class EquipmentController : MonoBehaviour
{
    [SerializeField] private Transform handSocket;
    [SerializeField] private RaycastInteractor raycastInteractor;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float range;
    [SerializeField] private PlayerAnimatorController animatorController;

    private IEquippable current;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            GameObject hitObject = raycastInteractor.PerformRaycast(range, layerMask);

            if (hitObject != null &&
                hitObject.TryGetComponent<IEquippable>(out var equippable))
            {
                Equip(equippable);
            }
            else
            {
                UnequipCurrent();
            }
        }
    }

    public void Equip(IEquippable newItem)
    {
        if (current == newItem) return;

        current?.Unequip();
        current = newItem;
        current.Initialise(animatorController);
        current.Equip(handSocket);
    }

    public void UnequipCurrent()
    {
        current?.Unequip();
        current = null;
    }
}

