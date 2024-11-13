using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{

    public enum InteractionType
    {
        Door,
        Button,
        Pickup
    }

    public InteractionType type;

    public InteractableName interactableName;

    public void Activate()
    {
        Debug.Log(interactableName.objectName + " was Touched");
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
