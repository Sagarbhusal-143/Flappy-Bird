using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace FlappyBird
{
    public enum GameState
    {
        Start,
        GamePlay,
        GamePause,
        Dead
    }

public class BirdController : MonoBehaviour
    {
       protected Rigidbody2D _rb;
        [SerializeField] protected float _forwardSpeed = 5f;
        [SerializeField] protected float _jumpStrength = 2f;
        [SerializeField] protected float _rotationSpeed = 10f;
        private Animator _anim;
        
        [ field:SerializeField] public GameState CurrentGameState { get; private set; } = GameState.GamePlay;

        public Action<GameState, GameState> OnGameStateChanged;
        protected virtual void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
            _rb.velocity = new Vector2(_forwardSpeed, 0f);
            _anim = GetComponent<Animator>();
        }

        public virtual void TransitionToState(GameState gameState)
        {
            switch (gameState)
            {
                case GameState.Start:
                {
                    _rb.velocity = new Vector2(_forwardSpeed, 0f);
                    break;
                }
                case GameState.GamePlay:
                case GameState.GamePause:
                case GameState.Dead:
                {
                    _rb.velocity = new Vector2(0f, 0f);
                    break;
                }
            }
            OnGameStateChanged?.Invoke(CurrentGameState, gameState);
            CurrentGameState = gameState;
        }
        protected virtual void Update()
        {
            
        }

        protected virtual void FixedUpdate()
        {
            
        }

        protected bool IsTap()
        {
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
            {
                AudioManager.Instance.PlayAudio(Audio.WingFlap);
                return true;
            }
            return false;
        }

        protected float ClampAngle(float angle, float min, float max)
        {
            return Mathf.Clamp((angle <= 180) ? angle : -(360 - angle), min, max);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if ( collision.isTrigger)
                return;
            if (CurrentGameState != GameState.GamePlay)
                return;
            
            TransitionToState(GameState.Dead);

            transform.DOMoveY(-2.3f, 0.4f).SetDelay(0.15f);
            transform.DORotate(new Vector3(0, 0, -90f), 0.2f).SetDelay(0.15f);
            _anim.enabled = false;
            AudioManager.Instance.PlayAudio(Audio.Die);
            AudioManager.Instance.PlayAudio(Audio.Hit);
        }
    }
}