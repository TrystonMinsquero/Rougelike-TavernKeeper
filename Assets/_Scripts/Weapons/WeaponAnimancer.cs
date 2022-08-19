using Animancer;
using UnityEngine;

namespace Weapons
{
    public class WeaponAnimancer : AnimancerComponent
    {
        public AnimationClip Idle;
        public AnimationClip Attacking;
        public AnimationClip Charging;
        public AnimationClip Charged;

        public AnimancerState Play(WeaponState state)
        {
            switch (state)
            {
                case WeaponState.Held: return Play(Idle);
                case WeaponState.Attacking: return Play(Attacking);
                case WeaponState.Charging: return Play(Charging);
                case WeaponState.Charged: return Play(Charged);
            }
            return null;
        }
    }
}