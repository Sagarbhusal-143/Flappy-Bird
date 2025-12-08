using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace FlappyBird
{
    public class Pipe : MonoBehaviour
    {
        [SerializeField] private Vector2 _pipedisplacement;

        private void Start()
        {
            transform.position += new Vector3(0f, Random.Range(_pipedisplacement.x, _pipedisplacement.y), 0f);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log(" Update score");
            UIManager.Instance.UpdateScore();
        }
    }
}