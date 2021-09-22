using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Mirror;

public class PlayerMovement : NetworkBehaviour
{
    [SerializeField] private PlayerInteractions interactions;

    [SerializeField] private NavMeshAgent agent;
    private new Camera camera;

    [SerializeField] private float rotateSpeedMovement = 0.1f;
    private float rotateVelocity;

    private float interactDistance = 4f;

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
            Move();
        }
    }

    private void Move()
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

            Debug.Log(hit.transform.name);

            IInteractable interactable = hit.transform.GetComponent<IInteractable>();

            Debug.Log(interactable);

            if (interactable != null)
            {
                float distance = Vector3.Distance(transform.position, hit.point);
                Debug.Log(distance);

                if(distance <= interactDistance)
                {
                    interactions.Interact(interactable);
                }
            }
        }
    }
}
