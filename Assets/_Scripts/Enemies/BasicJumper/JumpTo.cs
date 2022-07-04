using System;
using System.Collections;
using UnityEngine;

namespace Enemies
{
    public class JumpTo : EnemyState
    {
        private MonoBehaviour _objToJump;
        private float _jumpSpeed;
        private Vector2 _direction;
        private float _distance;
        
        public bool IsJumping { get; private set; }
        

        public JumpTo(MonoBehaviour objToJump, Vector2 direction, float jumpSpeed, float distance)
        {
            _objToJump = objToJump;
            _jumpSpeed = jumpSpeed;
            _direction = direction;
            _distance = distance;
            IsJumping = true;
        }

        public override void Tick()
        {
            
        }

        public override void OnEnter()
        {
            _objToJump.StartCoroutine(JumpToward(() => IsJumping = false));
        }

        public override void OnExit()
        {
            _objToJump.StopAllCoroutines();
        }

        private IEnumerator JumpToward(Action onFinished)
        {
            Transform t = _objToJump.transform;
            for (float i = 0; i < _distance; i += _jumpSpeed * Time.deltaTime)
            {
                t.Translate(_jumpSpeed * _direction * Time.deltaTime);
                yield return null;
            }

            onFinished();

        }
    }
}