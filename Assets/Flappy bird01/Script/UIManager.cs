using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FlappyBird
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance
        {
            get;
            private set;
            
        }

        private void Awake()
        {
            Instance = this;
        }

        [Header("GamePlay")] 
        [SerializeField] private TextMeshProUGUI _score;
        [SerializeField] private TextMeshProUGUI _highscore;
        private int _currentScore = 0;
        [SerializeField] private Image _whiteFlash;
        public BirdController CurrentBird;

        private void Start()
        {
            CurrentBird.OnGameStateChanged += OnGameStateChanged;
            _highscore.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
        }

        private void OnGameStateChanged(GameState preState, GameState postState)
        {
            if (postState == GameState.Dead)
            {
                _whiteFlash.DOFade(1f, 0.15f).SetLoops(2, LoopType.Yoyo);

                if (_currentScore > PlayerPrefs.GetInt("HighScore", 0))
                {
                    PlayerPrefs.SetInt("HighScore", _currentScore);
                    _highscore.text = _currentScore.ToString();
                }
            }
        }
        
        public void UpdateScore()
        {
            _currentScore++;
            _score.text = _currentScore.ToString();
            AudioManager.Instance.PlayAudio(Audio.Score);
        }

        private void OnDestroy()
        {
            CurrentBird.OnGameStateChanged -= OnGameStateChanged;
        }
    }
}