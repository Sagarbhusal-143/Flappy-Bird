using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlappyBird
{
    public class BirdControllerKinematic : BirdController
    {
       [SerializeField] private float _gravity = 10f;
        private float _verticalVelocity = 0f;
        protected override void Update()
        {
            if (CurrentGameState != GameState.GamePlay)
                return;
            
            if (IsTap())
                _verticalVelocity = _jumpStrength;

            float newZRotation = transform.rotation.eulerAngles.z +
                                 (_verticalVelocity >= 0f ? _rotationSpeed : -_rotationSpeed) * Time.deltaTime;
            transform.rotation = Quaternion.Euler(0f, 0f, ClampAngle(newZRotation, -30f, 30f));
        }

        protected override void FixedUpdate()
        {
            if (CurrentGameState != GameState.GamePlay)
                return;
            
            _verticalVelocity -= _gravity * Time.deltaTime;
            transform.position += new Vector3(_forwardSpeed * Time.deltaTime, _verticalVelocity);
        }
    }
    
}