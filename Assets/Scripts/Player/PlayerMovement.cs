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

    [SerializeField] private float interactDistance = 2.5f;
    private bool isLastInputInteractable = false;
    private IInteractable lastInteractableClicked = null;

    public override void OnStartLocalPlayer()
    {
        camera = Camera.main;
    }

    public override void OnStartClient()
    {
        enabled = isLocalPlayer;
    }
    void Update()
    {
        if (!agent.enabled) return;

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

            IInteractable interactable = hit.transform.GetComponent<IInteractable>();

            if (interactable != null)
            {
                if (lastInteractableClicked == interactable)
                {
                    return;
                }
                else
                {
                    StopAllCoroutines();
                }

                isLastInputInteractable = true;
                lastInteractableClicked = interactable;
                StartCoroutine(WaitForInteractDistance(hit.transform.gameObject ,interactable, hit.point));
            }
            else
            {
                isLastInputInteractable = false;
            }
        }
    }

    IEnumerator WaitForInteractDistance(GameObject interactableGameobject, IInteractable interactable, Vector3 interactablePosition)
    {
        while (isLastInputInteractable)
        {
            if(interactableGameobject == null) //another player took the interactable object
            {
                isLastInputInteractable = false;
                agent.SetDestination(transform.position);
                yield return null;
            }

            float distance = Vector3.Distance(transform.position, interactablePosition);

            if (distance > interactDistance)
            {
                yield return null;
            }
            else
            {
                isLastInputInteractable = false;
                interactions.Interact(interactable);
            }
        }

        lastInteractableClicked = null;
    }

    [TargetRpc]
    public void TargetToggleAgent(bool toggle)
    {
        agent.enabled = toggle;
    }
}
