using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckSpawn : MonoBehaviour
{
    EnemySpawner spawnLocation;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Spawn")
        {
            spawnLocation = other.GetComponent<EnemySpawner>();
            spawnLocation.HasSpawned();
        }
    }

    public void ReduceSpawn()
    {
        spawnLocation.ReduceSpawnCount();
    }
}
