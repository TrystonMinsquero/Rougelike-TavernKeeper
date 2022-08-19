using System;
using System.Linq;
using UnityEngine;

namespace Weapons
{
    [System.Serializable]
    public class Charge
    {
        public float ChargeProgress => chargeTime / ChargeLength;
        public bool IsCharged { get; private set; }
        public bool IsCharging => chargeTime > 0;
    
        public float MultiplierValue => chargeCurve.Evaluate(chargeTime / ChargeLength);
        
        public void AddChargeProgress(float deltaTime)
        {
            chargeTime += deltaTime;

            // don't let charge time get below 0
            chargeTime = chargeTime < 0 ? 0 : chargeTime;
            
            if (ChargeProgress >= 1)
            {
                IsCharged = true;
                ChargedUp.Invoke(MultiplierValue);
                Reset();
            }
        }

        /// <summary>
        /// Curve based on how long the charge can be held and the multiplied value that occurs
        /// </summary>
        [SerializeField] private AnimationCurve chargeCurve;
        // [SerializeField] private float canHoldTime = Single.PositiveInfinity;
        
        private float ChargeLength => chargeCurve.keys.Last().time;
        private float chargeTime = 0;
        
        public void Reset()
        {
            chargeTime = 0;
            IsCharged = false;
        }
        
        public event Action<float> ChargedUp = delegate(float f) {  };
        
    }
}