using UnityEngine;

namespace Interactables.Interfaces
{
    public interface IEquippable
    {
        public void Initialise(PlayerAnimatorController anim);
        public void Equip(Transform trans);
        public void Unequip();
    }
}