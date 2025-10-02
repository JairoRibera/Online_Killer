using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class EnemyMovimiento : MonoBehaviour
{
    public GameObject[] points;
    public float speed = 5f;
    public float contadorEspera = 0;
    public float tiempoEspera = 5;
    public bool cerca = false;
    public float distancia;
    private Vector3 target; 
    // Start is called before the first frame update
    void Start()
    {
        points = GameObject.FindGameObjectsWithTag("RandomPoint");
        contadorEspera = tiempoEspera;
        FindRandomPoint();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        distancia = Vector3.Distance(transform.position,target);

        if (distancia <= 0.1f)
        {
            contadorEspera -= Time.deltaTime;
            if( contadorEspera <= 0)
            {
                FindRandomPoint();
                contadorEspera = tiempoEspera;
            }
        }
    }
    public void FindRandomPoint()
    {
        int _randomIndex = Random.Range(0, points.Length);
        target = points[_randomIndex].transform.position;
        //transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        distancia = Vector3.Distance(transform.position, points[_randomIndex].transform.position);

    }
}
