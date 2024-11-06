using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;
    [SerializeField] private LayerMask cubeLayerMask;
    [SerializeField] private LayerMask ground;


    private void Awake()
    {
        playerCamera = Camera.main;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float distance = 10;

        Debug.DrawLine(playerCamera.transform.position, playerCamera.transform.forward * 20, Color.red);

        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out RaycastHit hit, 20, ground.value))
        {
            distance = hit.distance;
        }

        RaycastHit[] hits = Physics.RaycastAll(playerCamera.transform.position, playerCamera.transform.forward, distance, cubeLayerMask.value);

        foreach (RaycastHit hitSingle in hits)
        {
            if (hitSingle.collider.TryGetComponent<Renderer>(out Renderer renderer))
            {
                renderer.material.color = Color.blue;
            }
        }


        //if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out RaycastHit hit, 20, cubeLayerMask.value))
        //{
        //    if (hit.collider.GetComponent<Renderer>())
        //    {
        //        Debug.Log("Hit");
        //        hit.collider.GetComponent<Renderer>().material.color = Color.blue;
        //    }
        //}
    }
}
