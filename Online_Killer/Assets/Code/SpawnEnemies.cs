using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    public GameObject[] spawnPoints;       // Puntos en el mapa donde pueden aparecer
    public float respawnTime = 5f;         // Tiempo de espera para reaparecer enemigos

    public List<GameObject> enemigosDesactivados = new List<GameObject>();

    void Start()
    {
        spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
    }
    void Update()
    {
        // Intentamos respawnear si hay enemigos desactivados
        if (enemigosDesactivados.Count > 0)
        {
            respawnTime -= Time.deltaTime;
            if (respawnTime <= 0f)
            {
                RespawnEnemy();
                respawnTime = 5f; // Reiniciamos contador
            }
        }
    }

    // Agregar enemigos a la lista de desactivados (lo llamas desde tu script de disparo)
    public void AddToRespawnList(GameObject enemigo)
    {
        if (!enemigosDesactivados.Contains(enemigo))
        {
            enemigosDesactivados.Add(enemigo);
            enemigo.SetActive(false);
        }
    }

    //  Reaparecer un enemigo en un punto aleatorio
    private void RespawnEnemy()
    {
        if (enemigosDesactivados.Count == 0) return;

        int randomEnemy = Random.Range(0, enemigosDesactivados.Count);
        int randomSpawn = Random.Range(0, spawnPoints.Length);

        GameObject enemigo = enemigosDesactivados[randomEnemy];
        Transform punto = spawnPoints[randomSpawn].transform;

        enemigo.transform.position = punto.position;
        enemigo.SetActive(true);

        enemigosDesactivados.RemoveAt(randomEnemy);
    }
}
