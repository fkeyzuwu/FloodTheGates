using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [HideInInspector] public Transform player;
    public CameraControlMode controlMode = CameraControlMode.FollowPlayer;
    public Vector3 cameraOffset = new Vector3(7, 10 , 0);

    [Range(0.01f, 1.0f)]
    [SerializeField] private float smoothness = 0.5f;

    void Update()
    {
        if (!player)
        {
            return;
        }

        switch (controlMode)
        {
            case CameraControlMode.FollowPlayer:
                Vector3 newPos = player.position + cameraOffset;
                transform.position = Vector3.Slerp(transform.position, newPos, smoothness);
                break;
            case CameraControlMode.Free:
                break;
            case CameraControlMode.Battle:
                break;
        }
    }
}
