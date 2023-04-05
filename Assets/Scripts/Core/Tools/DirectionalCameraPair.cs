using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using Core.Enums;

[Serializable]
public class DirectionalCameraPair
{ 
    [SerializeField] private CinemachineVirtualCamera _rightCamera; 
    [SerializeField] private CinemachineVirtualCamera _leftCamera;

    private Dictionary<Direction, CinemachineVirtualCamera> _directionalCameras;

    public Dictionary<Direction, CinemachineVirtualCamera> DirectionalCamera
    {
        get
        {
            if (_directionalCameras != null)
                return _directionalCameras;
            _directionalCameras = new Dictionary<Direction, CinemachineVirtualCamera>
            {
                { Direction.Right, _rightCamera },
                { Direction.Left, _leftCamera }
            };
            return _directionalCameras;

        }
    }

}
