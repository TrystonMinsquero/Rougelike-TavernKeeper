using System.Collections;
using UnityEngine;

namespace Weapons
{
    public interface IAttackType
    {
        public IEnumerator AttackRoutine(WeaponMB weaponObj, Vector2 direction, System.Action onFinished=null);
    }
}