using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
public class EnemyMovimiento : MonoBehaviour
{
    Camera cam;
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
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        //if (target == Vector3.zero) return; // si no hay punto activo, no hacer nada
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        transform.LookAt(cam.transform.position);
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
        //// Buscar todos los puntos activos *en este momento exacto*
        //var puntosActivos = GameObject.FindGameObjectsWithTag("RandomPoint")
        //    .Where(p => p.activeInHierarchy)
        //    .ToArray();

        //if (puntosActivos.Length == 0)
        //{
        //    Debug.LogWarning(" No hay puntos activos para moverse.");
        //    target = Vector3.zero; // sin destino válido
        //    return;
        //}

        int _randomIndex = Random.Range(0, points.Length);
        target = points[_randomIndex].transform.position;
        //transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        distancia = Vector3.Distance(transform.position, points[_randomIndex].transform.position);

    }
}
