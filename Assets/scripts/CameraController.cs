using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{

    public CinemachineVirtualCamera virtualCamera;
    private void Awake()
    {
        virtualCamera = this.GetComponent<CinemachineVirtualCamera>();
    }
    public void SetCameraTarget (GameObject target)
    {
        virtualCamera.Follow = target.transform;
    }
}
