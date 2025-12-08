using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace FlappyBird
{
    public class BirdControllerPhysics : BirdController
    {
       
      
        protected override void Update()
        {
            if(IsTap())
                _rb.velocity = new Vector2(_forwardSpeed, _jumpStrength);

            float newZRotation = transform.rotation.eulerAngles.z +
                                 (_rb.velocity.y >= 0f ? _rotationSpeed : -_rotationSpeed) * Time.deltaTime;
            transform.rotation = Quaternion.Euler(0f, 0f, ClampAngle(newZRotation, -30f, 30f));
        }
        
    }
}