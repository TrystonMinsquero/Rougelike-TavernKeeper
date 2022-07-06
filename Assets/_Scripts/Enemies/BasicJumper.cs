using System;
using System.Collections;
using Misc;
using UnityEngine;

namespace Enemies
{
    public enum BasicJumperState 
    {
        Rest,
        JumpToward,
        LungeToward,
        ChargeUp,
        Charging,
        Jumping
    }
    
    public class BasicJumper : Enemy
    {
        public BasicJumperState currentState = BasicJumperState.Rest;

        [Header("Jumper Attributes")]
        // public float jumpPower = 10f;
        // public float jumpDampening = 1;
        public AnimationCurve jumpCurve;
        public float restTimeAfterJump = 1f;
        public float chargeTime = .8f;
        // public float lungePower = 20f;
        // public float lungeDampening = 1;
        public AnimationCurve lungeCurve;
        public float lungeAttackRange = 3f;
        public float restTimeAfterLunge = 3f;


        // conditionals
        private bool _isJumping;
        private bool _isCharging;
        private bool _isResting;

        private SpriteRenderer sr;

        private void Start()
        {
            
            sr = GetComponent<SpriteRenderer>();
        }

        public void Update()
        {
            currentState = GetNextState(currentState);
            SetAnimationState(currentState);
        }

        private void SetAnimationState(BasicJumperState state)
        {
            switch (state)
            {
                case BasicJumperState.Rest:
                    sr.color = Color.green;
                    break;
                case BasicJumperState.Charging:
                    sr.color = Color.yellow;
                    break;
                case BasicJumperState.Jumping:
                    sr.color = Color.red;
                    break;
            }
        }
        

        private BasicJumperState GetNextState(BasicJumperState state)
        {
            switch (state)
            {
                // Action states
                case BasicJumperState.JumpToward:
                    Debug.Log("Jump");
                    JumpTowardPlayer(jumpCurve, restTimeAfterJump);
                    return BasicJumperState.Jumping;
                
                case BasicJumperState.LungeToward:
                    Debug.Log("Lunge");
                    JumpTowardPlayer(lungeCurve, restTimeAfterLunge);
                    return BasicJumperState.Jumping;
                
                // Waiting States
                case BasicJumperState.Rest:
                    if (target == null || _isResting)
                        return BasicJumperState.Rest;
                    if (PlayerDistance < lungeAttackRange)
                        return BasicJumperState.ChargeUp;
                    else
                        return BasicJumperState.JumpToward;
                
                case BasicJumperState.ChargeUp:
                    _isCharging = true;
                    StartCoroutine(DoAfter(chargeTime, () => _isCharging = false));
                    return BasicJumperState.Charging;
                
                case BasicJumperState.Charging:
                    return _isCharging ? BasicJumperState.Charging : BasicJumperState.LungeToward;
                
                case BasicJumperState.Jumping:
                    return _isJumping ? BasicJumperState.Jumping : BasicJumperState.Rest;
            }
    
            // Shouldn't get here
            Debug.LogWarning($"{state} is not accounted for in {name}");
            return state;
        }

        // private void JumpTowardPlayer(float speed, float distance, float restTime)
        // {
        //     _isJumping = true;
        //     StartCoroutine(JumpToward(PlayerDirection, speed, distance,
        //         () =>
        //         {
        //             _isJumping = false;
        //             _isResting = true;
        //             StartCoroutine(DoAfter(restTime, () => _isResting = false));
        //         }));
        // }
        
        private void JumpTowardPlayer(AnimationCurve curve, float restTime)
        {
            _isJumping = true;
            StartCoroutine(JumpToward(PlayerDirection, curve,
                () =>
                {
                    _isJumping = false;
                    _isResting = true;
                    StartCoroutine(DoAfter(restTime, () => _isResting = false));
                }));
        }

        private IEnumerator DoAfter(float time, Action action)
        {
            yield return new WaitForSeconds(time);
            action();
        }

        // private IEnumerator JumpToward(Vector2 direction, float initVelocity, float dampening, Action onFinish = null)
        // {
        //     _rb.velocity = direction.normalized * initVelocity;
        //     while (_rb.velocity.magnitude > .01f)
        //     {
        //         _rb.velocity -= _rb.velocity.normalized * dampening * Time.deltaTime;
        //         yield return null;
        //     }
        //     _rb.velocity -= Vector2.zero;
        //     
        //     if (onFinish != null)
        //         onFinish();
        // }
        
        private IEnumerator JumpToward(Vector2 direction, AnimationCurve curve, Action onFinish = null)
        {
            float length = curve.keys[curve.keys.Length - 1].time;
            float startTime = Time.time;
            Vector2 initPosition = transform.position;

            while (Time.time < startTime + length)
            {
                var newPos = (curve.Evaluate(Time.time - startTime) * direction) + initPosition;
                _rb.MovePosition(newPos);
                yield return null;
            }

            // transform.position = curve.keys[curve.keys.Length - 1].value * direction;
            if (onFinish != null)
                onFinish();
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            
            if (!_isJumping)
                return;
            if (col.gameObject.CompareTag("Player") && col.gameObject.TryGetComponent<Health>(out var health))
            {
                health.Damage(damage);
            }
        }
    }
}