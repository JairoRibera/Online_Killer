using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    public bool canSpawn = false;
    public float contadorSpawn = 0;
    private float tiempoSpawn = 5;
    public GameObject enemy;

    // Start is called before the first frame update
    void Start()
    {
        contadorSpawn = tiempoSpawn;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SpawnEnemy()
    {
        if (canSpawn == true)
        {
            contadorSpawn -= Time.deltaTime;
            if (contadorSpawn <= 0)
            {
                Instantiate(enemy);
                contadorSpawn = tiempoSpawn;
            }

        }
    }
}
