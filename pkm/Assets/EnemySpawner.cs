using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    float[] percentage;
    [SerializeField]
    GameObject[] enemyList;

    [SerializeField]
    int maxSpawn;
    [SerializeField]
    int spawned;
    [SerializeField]
    int minLevel, maxLevel;

    Renderer render;

    // Start is called before the first frame update
    void Start()
    {
        render = GetComponent<Renderer>();
        //to do: check if visible to camera, if true, spawn random enemy within list x amount of times until max spawn rate has been reached. check again to see if max enemies are available after x time passed, spawn again if theres space.
    }

    //Issue: ENemies spawning straight away when killed


    // Update is called once per frame
    void LateUpdate()
    {
        if(render.isVisible)
        {
            InvokeRepeating(nameof(Spawn), 2f, 10);
            //Debug.Log("Is Visible");
        }
        else if(!render.isVisible || spawned >= maxSpawn)
        {
            //Debug.Log("Not Visible");
            //CancelInvoke();
        }
    }


    private void Spawn()
    {
        //Debug.Log("Spawn");
        if (spawned < maxSpawn)
        {
            HasSpawned();
            Bounds bounds = GetComponent<Collider>().bounds;
            float offsetX = Random.Range(-bounds.extents.x, bounds.extents.x);
            float offsetY = Random.Range(-bounds.extents.y, bounds.extents.y);
            float offsetZ = Random.Range(-bounds.extents.z, bounds.extents.z);

            GameObject newEnemy = GameObject.Instantiate(enemyList[GetRanndom()]);
            newEnemy.transform.position = bounds.center + new Vector3(offsetX, 0, offsetZ);
            Stats stats = newEnemy.GetComponent<Stats>();
            stats.level = Random.Range(minLevel, maxLevel);
            stats.SetHponSpwan();
            stats.UpdateInfo();
        }
        else
        {
            return;
        }
    }

    public void HasSpawned()
    {
        spawned++;
        if(spawned >= maxSpawn)
        {
            spawned = maxSpawn;
        }
    }

    public void ReduceSpawnCount()
    {
        spawned--;
        if(spawned <= 0)
        {
            spawned = 0;
        }
    }
    private int GetRanndom()
    {
        float random = Random.Range(0f, 1f);
        float numToAdd = 0;
        float total = 0;
        for (int i = 0; i < percentage.Length; i++)
        {
            total += percentage[i];
        }

        for (int i = 0; i < enemyList.Length; i++)
        {
            if(percentage[i] / total + numToAdd >= random)
            {
                return i;
            }
            else
            {
                numToAdd += percentage[i] / total;
            }
        }
        return 0;
    }
}