using System;
using Unity.Cinemachine;
using UnityEngine;

[Serializable]
public class CameraSettings
{
    [SerializeField] private CinemachineCamera _cinemachineCameraPrefab;
    [SerializeField] private Transform _cameraTargetPrefab;
    

    public CinemachineCamera CinemachineCameraPrefab => _cinemachineCameraPrefab;
    public Transform CameraTargetPrefab => _cameraTargetPrefab;
    
}