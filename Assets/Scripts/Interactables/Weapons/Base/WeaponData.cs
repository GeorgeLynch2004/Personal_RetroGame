using UnityEngine;

namespace Interactables.Weapons
{
    [CreateAssetMenu(menuName = "ScriptableObjects/WeaponData")]
    public class WeaponData: ScriptableObject
    {
        public string weaponName;
        public float cooldown;
        public GameObject prefab;
        public AudioClip useSound;
        public RuntimeAnimatorController overrideController;
        public float range;
        public float damage;
        public LayerMask layerMask;
    }
}