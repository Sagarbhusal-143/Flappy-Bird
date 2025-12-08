using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlappyBird
{

    public class EnvironmentManager : MonoBehaviour
    {
        [Header("Background Environment")] [SerializeField]
        private Transform[] _environmentBackGrounds = new Transform[3];

        [SerializeField] private Transform _recycleMarker;
        private Transform _endPoint;
        private int _environmentIndex = 1;

        [Header("Pipe")] [SerializeField] private GameObject _pipePrefab;
        [SerializeField] private float _pipeSpawnGap = 2.5f;
        [SerializeField] private float _pipeLastSpawnedPosition = 5f;
        private bool _canSpawnPipe = true;

        private void Start()
        {
            _endPoint = _environmentBackGrounds[_environmentIndex]
                .GetChild(_environmentBackGrounds[_environmentIndex].childCount - 1);
        }

        private void Update()
        {
            //recycle environment
            if (_recycleMarker.position.x > _endPoint.position.x)
            {
                int endpointIndex = (_environmentIndex + 2) % 3;
                Transform lastEnv = _environmentBackGrounds[endpointIndex];
                Transform lastEndPoint = lastEnv.GetChild(lastEnv.childCount - 1);
                _environmentBackGrounds[_environmentIndex].position = lastEndPoint.position;
                _environmentIndex = endpointIndex++ % 3;
                _endPoint = _environmentBackGrounds[_environmentIndex]
                    .GetChild(_environmentBackGrounds[_environmentIndex].childCount - 1);
            }
            
            //Instantiate Pipes
            if (_canSpawnPipe && _recycleMarker.position.x > _pipeLastSpawnedPosition)
            {
                _pipeLastSpawnedPosition += _pipeSpawnGap;
                Instantiate(_pipePrefab, new Vector3(_pipeLastSpawnedPosition, 0f), Quaternion.identity);
            }
        }
    }
}