using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitSlice : MonoBehaviour
{
    public GameObject whole;
    public GameObject sliced;

    Rigidbody fruitRigidbody;
    Collider fruitCollider;

    ParticleSystem fruitParticle;


  
    private void Awake()
    {
        fruitRigidbody = GetComponent<Rigidbody>();
        fruitCollider = GetComponent<Collider>();
        fruitParticle = GetComponentInChildren<ParticleSystem>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            
            Blade blade = other.GetComponent<Blade>();
            Slice(blade.direction , blade.transform.position , blade.sliceForce);
        }
    }
    private void Slice(Vector3 direction , Vector3 position , float sliceForce )
    {
        FindAnyObjectByType<GameManager>().IncreaseScore();
        whole.SetActive(false);
        fruitCollider.enabled = false;

        sliced.SetActive(true);
        fruitParticle.Play();

        float angle = Mathf.Atan2(direction.y , direction.x) * Mathf.Rad2Deg;
        sliced.transform.rotation = Quaternion.Euler(0, 0, angle); 
        Rigidbody[] slice = sliced.GetComponentsInChildren<Rigidbody>();

        foreach (Rigidbody s in slice)
        {
            s.velocity = fruitRigidbody.velocity;
            s.AddForceAtPosition(direction * sliceForce , position , ForceMode.Impulse);
        }
    }

}
