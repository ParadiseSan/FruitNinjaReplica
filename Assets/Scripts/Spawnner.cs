using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Unity.VisualScripting;
using UnityEngine;

public class Spawnner : MonoBehaviour
{
    [SerializeField] GameObject[] FruitsPrefab;
    [SerializeField] GameObject bombPrefab;
    public float destroyTime = 5f;

    public float min_spawnTime = 0.25f;
    public float max_spawnTime = 1f;

    public float min_Angle = -15f;
    public float max_Angle = 15f;

    public float min_force = 18f;
    public float max_force = 22f;

    Collider col;

    [Range  (0f, 1f)] 
    public float bombProbability = 0.05f;

    private void Awake()
    {
        col = GetComponent<Collider>();
    }

    private void OnEnable()
    {
        StartCoroutine(Spawn());    
    }

    void OnDisable()
    {
        StopAllCoroutines();
    }
    IEnumerator Spawn() 
    { 
      while(enabled) 
        {
            GameObject prefabFruit = FruitsPrefab[Random.Range(0 , FruitsPrefab.Length)];
            if(Random.value < bombProbability)
            {
                prefabFruit = bombPrefab;
            }

            Vector3 position = new Vector3();

            position.x = Random.Range(col.bounds.min.x , col.bounds.max.x);
            position.y = Random.Range(col.bounds.min.y, col.bounds.max.y);
            position.z = Random.Range(col.bounds.min.z, col.bounds.max.z);

            Quaternion rot = Quaternion.Euler(0,0, Random.Range(min_Angle , max_Angle));

            GameObject fruitSpawned = Instantiate(prefabFruit, position , rot);
            Destroy(fruitSpawned, destroyTime);

            float forceValue = Random.Range(min_force , max_force);

            fruitSpawned.GetComponent<Rigidbody>().AddForce(fruitSpawned.transform.up * forceValue,ForceMode.Impulse);

            yield return new WaitForSeconds(Random.Range(min_spawnTime , max_spawnTime));
        }
    
    
    }
}
