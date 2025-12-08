using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace FlappyBird
{
    public class CameraFollower : MonoBehaviour
    {
        [SerializeField] private Transform _player;
        [SerializeField] private float _flyHeight;

        private void Update()
        {
            transform.position = new Vector3(_player.position.x, _flyHeight, -10f);
        }
    }
}
