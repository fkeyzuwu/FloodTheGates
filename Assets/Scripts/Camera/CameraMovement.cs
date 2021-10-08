using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private new Camera camera;
    [HideInInspector] public Transform player;
    [SerializeField] private CameraControlMode controlMode = CameraControlMode.FollowPlayer;

    [Range(0.01f, 1.0f)]
    [SerializeField] private float smoothness = 0.5f;

    [Header("Map Cam Settings")]
    [SerializeField] private Vector3 mapCameraOffset;
    [SerializeField] private Vector3 mapCameraRotation;
    [SerializeField] private float mapFOV = 55f;

    [Header("Battle Cam Settings")]
    [SerializeField] private Vector3 battleCameraPosition;
    [SerializeField] private Vector3 battleCameraRotation;
    [SerializeField] private float battleFOV = 40f;

    void Start()
    {
        camera = GetComponent<Camera>();
    }

    void Update()
    {
        if (!player)
        {
            return;
        }

        switch (controlMode)
        {
            case CameraControlMode.FollowPlayer:
                Vector3 newPos = player.position + mapCameraOffset;
                transform.position = Vector3.Slerp(transform.position, newPos, smoothness);
                break;
            case CameraControlMode.Free:
                break;
            case CameraControlMode.Battle:
                break;
        }
    }

    public void SetupCamera(CameraControlMode switchMode)
    {
        switch (switchMode)
        {
            case CameraControlMode.FollowPlayer:
                controlMode = CameraControlMode.FollowPlayer;
                transform.position = player.position + mapCameraOffset;
                transform.rotation = Quaternion.Euler(mapCameraRotation);
                camera.fieldOfView = mapFOV;
                break;
            case CameraControlMode.Free:
                controlMode = CameraControlMode.Free;
                break;
            case CameraControlMode.Battle:
                controlMode = CameraControlMode.Battle;
                transform.position = battleCameraPosition;
                transform.rotation = Quaternion.Euler(battleCameraRotation);
                camera.fieldOfView = battleFOV;
                break;
        }
    }

    public void InitializeCamera(Transform playerTransform)
    {
        player = playerTransform;
        SetupCamera(CameraControlMode.FollowPlayer);
    }
}
