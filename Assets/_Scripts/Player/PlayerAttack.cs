using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons;

public class PlayerAttack : MonoBehaviour
{
    public WeaponMB Weapon;

    private float lastHitTime = 0;


    public void Attack(DirectionInfo directionInfo)
    {
        if (!Weapon)
            return;
        Vector2 dir = directionInfo.lookDirection.magnitude > .1f ? directionInfo.lookDirection : directionInfo.moveDirection;
        Weapon?.Use(dir);
    }
}
