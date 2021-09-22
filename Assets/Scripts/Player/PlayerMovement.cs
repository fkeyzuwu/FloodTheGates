using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Mirror;

public class PlayerMovement : NetworkBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    private new Camera camera;

    [SerializeField] private float rotateSpeedMovement = 0.1f;
    private float rotateVelocity;

    public override void OnStartLocalPlayer()
    {
        camera = Camera.main;
        camera.GetComponent<CameraMovement>().player = transform; //injecting player into non networked object
        camera.transform.position = transform.position + camera.GetComponent<CameraMovement>().cameraOffset;
        //really bad camera injection shit but whatever good enough for now, later gotta fix
    }
    void Update()
    {
        if (!isLocalPlayer) return;

        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;

            if (Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity))
            {
                agent.SetDestination(hit.point);

                Quaternion rotationToLookAt = Quaternion.LookRotation(hit.point - transform.position);
                float rotationY = Mathf.SmoothDampAngle(
                    transform.eulerAngles.y,
                    rotationToLookAt.eulerAngles.y,
                    ref rotateVelocity,
                    rotateSpeedMovement * (Time.deltaTime * 5)
                    );

                transform.eulerAngles = new Vector3(0, rotationY, 0);
            }
        }
    }
}
