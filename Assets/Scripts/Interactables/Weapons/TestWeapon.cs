using System;
using UnityEngine;

namespace Interactables.Weapons
{
    public class TestWeapon : Weapon
    {
        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Use();
            }
        }
    }
}
