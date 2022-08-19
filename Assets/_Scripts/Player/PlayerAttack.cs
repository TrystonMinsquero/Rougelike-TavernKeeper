using System;
using System.Collections;
using System.Collections.Generic;
using Animancer.Examples.StateMachines.Weapons;
using UnityEngine;
using Weapons;

public class PlayerAttack : MonoBehaviour
{
    public WeaponMB Weapon;
    public bool AttackInput { get; set; }
    public DirectionInfo DirectionInfo { get; set; }

    private void Update()
    {
        if (AttackInput && Weapon)
        {
            Vector2 dir = DirectionInfo.lookDirection.magnitude > .1f ? DirectionInfo.lookDirection : DirectionInfo.moveDirection;
            Weapon.Use(dir);
        }
    }

    // public void Attack(DirectionInfo directionInfo)
    // {
    //     if (!Weapon)
    //         return;
    //     Vector2 dir = directionInfo.lookDirection.magnitude > .1f ? directionInfo.lookDirection : directionInfo.moveDirection;
    //     Weapon?.Use(dir);
    // }
}
