using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blade : MonoBehaviour
{
    TrailRenderer bladeTrail;
    Camera cam;
    Collider bladeCollider;
    bool slicing = false;
    Rigidbody rb;
    Vector3 previousPos;

    public float sliceForce = 10f;

    public Vector3 direction { get; private set; }

    private void Awake()
    {
        cam = Camera.main;
        bladeCollider = GetComponent<Collider>();
        bladeTrail = GetComponentInChildren<TrailRenderer>();
       
    }
    private void OnEnable()
    {
       SlicingStopped();
    }

    private void OnDisable()
    {
        SlicingStopped();
    }
    private void Update()
    {
      
        if(Input.GetMouseButtonDown(0)) 
        {
            
        StartedSlicing();
        }
        else if(Input.GetMouseButtonUp(0)) 
        {
            
           SlicingStopped();
       
        }

        else if(slicing)
        {
            
            ContinueSlicing();
        }
    }

    private void StartedSlicing()
    {
       
        Vector3 PositionA = cam.ScreenToWorldPoint(Input.mousePosition);
        PositionA.z = 0f;
        transform.position = PositionA;

        slicing = true;

        bladeCollider.enabled = true;

        bladeTrail.enabled = true;
        bladeTrail.Clear();
    }

    private void SlicingStopped()
    {
        slicing = false;
        bladeCollider.enabled = false;
        bladeTrail.enabled = false;
    }

    void ContinueSlicing()
    {
       
        Vector3 newPosition = cam.ScreenToWorldPoint(Input.mousePosition);
        newPosition.z = 0f;

        direction = newPosition - transform.position;
        //Debug.Log("Direction : "+direction);
        
         float velocity = direction.magnitude / Time.fixedDeltaTime;
        //Debug.Log("Velocity : "+velocity);

        bladeCollider.enabled = velocity > 5f; 
        transform.position = newPosition;
    }

    
}
