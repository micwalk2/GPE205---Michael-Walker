using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawn : MonoBehaviour
{
    public GameObject pickupPrefab;
    public float spawnDelay;

    private GameObject spawnedPickup;
    private Transform tf;
    private float nextSpawnTime;

    // Start is called before the first frame update
    void Start()
    {
        nextSpawnTime = Time.time + spawnDelay;
    }

    // Update is called once per frame
    void Update()
    {
        // If there isn't a pickup spawned...
        if (spawnedPickup == null)
        {
            // ...and it is time to spawn a new one...
            if (Time.time >= nextSpawnTime)
            {
                // ...spawn a new pickup and set the next spawn time
                spawnedPickup = Instantiate(pickupPrefab, transform.position, Quaternion.identity);
                nextSpawnTime = Time.time + spawnDelay;
            }
        }
        else
        {
            // ...otherwise, the pickup still exists; postpone the next spawn time
            nextSpawnTime = Time.time + spawnDelay;
        }
    }
}
