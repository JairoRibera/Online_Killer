using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Shoot : MonoBehaviour
{
    private Camera cam;
    public float maxDistance = 10f;
    public LayerMask Enemy;
    public Text textScore;
    private Score _scoreRef;
    private float puntuacion = 0;
    public Text balaText;
    //[Header("Balas y Recarga")]
    public int cargador = 5;
    public int bullet = 5;
    public bool canShoot = true;
    //public bool isreloading = false;
    private float reloadTiempo = 10f;
    public float reloadContador = 0f;
    private float cargaRapidaTiempo = 5f;
    public float cargaRapidaContador = 0f;
    public bool cargaRapida = false;
    public string Recarga = "Recarga";

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        cargador = bullet;
        reloadContador = reloadTiempo;
        cargaRapidaContador = cargaRapidaTiempo;
    }

    // Update is called once per frame
    void Update()
    {
        Disparo();
        Mostrartexto();
        RecargaRapida();
        Recargar();
    }
    private void Disparo()
    {

        if (canShoot == true && Input.touchCount > 0)
        {
            //Si el contador Touch es mayor a 1
                //Guarda el input del primer dedo
                Touch touch = Input.GetTouch(0);
                //Restamos 1 a la municion
                //Empezamos la fase Began
                if (Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    bullet--;
                    //Lanzamos un ray desde la dirección del dedo
                    Ray ray = cam.ScreenPointToRay(Input.GetTouch(0).position);
                    RaycastHit hit;
                    //Si el rayo choca con un objeto con la layer enemy
                    if (Physics.Raycast(ray, out hit, 10, Enemy))
                    {
                        //Obtenemos el Script Score y desactivamos el objeto
                        _scoreRef = hit.collider.gameObject.GetComponent<Score>();
                        hit.collider.gameObject.SetActive(false);
                        //SI el enemigo tiene el Script le sumamos el numero a la puntuacion
                        if (_scoreRef != null)
                        {
                            SpawnEnemies spawner = FindObjectOfType<SpawnEnemies>();
                            ComboKill combo = FindObjectOfType<ComboKill>();
                            int multiplicador = 1;

                            if (combo != null)
                            {
                            multiplicador = combo.AddKill(); // Obtenemos el multiplicador actual
                            }
                            puntuacion = puntuacion + _scoreRef.score;

                            puntuacion = puntuacion + (_scoreRef.score * multiplicador);
                            if (spawner != null)
                            {
                                spawner.AddToRespawnList(hit.collider.gameObject);
                            }

                    }
                        else return;
                    }

                }

        }
        if (bullet <= 0 && !cargaRapida && canShoot)
        {
            canShoot = false;
            cargaRapida = true;
        }


    }
    public void RecargaRapida()
    {
        if (cargaRapida == true)
        {
            cargaRapidaContador -= Time.deltaTime;
            Debug.Log("Recarga Rapida");
            if (cargaRapidaContador <= 0)
            {
                Debug.Log("Perdiste la carga rapida");
                cargaRapida = false;
                cargaRapidaContador = cargaRapidaTiempo;
            }
            if (cargaRapidaContador > 0 && SimpleInput.GetButtonDown(Recarga))
            {
                reloadContador = 0;
                bullet = cargador;
                canShoot = true;
                cargaRapidaContador = cargaRapidaTiempo;
                cargaRapida = false;
            }

        }
    }
    public void Recargar()
    {
        if(canShoot == false)
        {
            reloadContador -= Time.deltaTime;
            if (reloadContador <= 0f)
            {
                bullet = cargador;
                canShoot = true;
                reloadContador = reloadTiempo;

            }
        }
    }
    public void Combo()
    {
        
    }
    private void Mostrartexto()
    {
        //El texto será igual a la puntuacion
        textScore.text = "Puntuación " + puntuacion.ToString();
        balaText.text = "6 / " + bullet.ToString();
    }
}
