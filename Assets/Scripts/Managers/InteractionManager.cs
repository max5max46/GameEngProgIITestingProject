using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    private CameraManager cameraManager;
    private Camera playerCamera;
    private UIManager uiManager;

    [Header("Properties")]
    [SerializeField] private int maxRayDistance;

    [HideInInspector] public bool interactionPossible;
    private GameObject target;

    
    private Interactable targetInteractable;

    private void Awake()
    {
        cameraManager = FindAnyObjectByType<CameraManager>();
        playerCamera = cameraManager.playerCamera;
        uiManager = FindAnyObjectByType<UIManager>();

    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
            interactionPossible = true;
        else
            interactionPossible = false;
    }

    private void FixedUpdate()
    {
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out RaycastHit hit, maxRayDistance))
        {
            if (hit.transform.gameObject.CompareTag("interactable"))
            {
                //Debug.Log("I'm peeping the " + hit.transform.gameObject.name);
                target = hit.transform.gameObject;
                targetInteractable = target.GetComponent<Interactable>();
            }
            else
            {
                target = null;
                targetInteractable = null;
            }
        }
        else
        {
            target = null;
            targetInteractable = null;
        }
        SetGameplayMessage();
    }

    public void Interact()
    {
        switch (targetInteractable.type)
        {
            case Interactable.InteractionType.Door:
                target.SetActive(false);
                Debug.Log("Opened Door");
                break;

            case Interactable.InteractionType.Button:
                Debug.Log("Button Pushed");
                break;

            case Interactable.InteractionType.Pickup:
                target.SetActive(false);
                Debug.Log("Picked Up Object");
                break;
        }
    }

    void SetGameplayMessage()
    {
        string message = "";
        if (target != null)
        {
            switch (targetInteractable.type)
            {
                case Interactable.InteractionType.Door:
                    message = "Press LMB to open Door";
                    break;

                case Interactable.InteractionType.Button:
                    message = "Press LMB to push Button";
                    break;

                case Interactable.InteractionType.Pickup:
                    message = "Press LMB to pickup Sphere";
                    break;
            }
        }

        uiManager.UpdateGameplayMessage(message);
    }
}
