using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
public class Shoot : MonoBehaviour
{
    private Camera cam;
    private float maxDistance = 100f;
    public LayerMask Enemy;
    public Text textScore;
    private Score _scoreRef;
    private float puntuacion = 0;
    public Text balaText;
    [Header("Balas y Recarga")]
    public int cargador = 6;
    public int bullet = 6;
    public bool canShoot = true;
    //public bool isreloading = false;
    private float reloadTiempo = 7f;
    public float reloadContador = 0f;
    private float cargaRapidaTiempo = 5f;
    public float cargaRapidaContador = 0f;
    public bool cargaRapida = false;
    public string Recarga = "Recarga";
    public int EnemigosEliminados;
    private  ComboKill combo;
    public bool comboActivado;
    [Header("Animacion y Particulas")]
    public GameObject SistemaParticulas;
    public Animator anim_Boton_Recarga;
    public GameObject Recarga_Text;
    public Animator anim_Text_Recarga;
    public GameObject MuerteAnim;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        cargador = bullet;
        reloadContador = reloadTiempo;
        cargaRapidaContador = cargaRapidaTiempo;
        balaText.text = "6 / " + bullet.ToString();
        combo = GetComponent<ComboKill>();
    }

    // Update is called once per frame
    void Update()
    {
        Mostrartexto();
        RecargaRapida();
        Recargar();
        Disparo();
    }
    private void Disparo()
    {

        if (canShoot == true && Input.touchCount > 0)
        {
            Recarga_Text.SetActive(false);
            //Si el contador Touch es mayor a 1
            //Guarda el input del primer dedo
            Touch touch = Input.GetTouch(0);
                //Restamos 1 a la municion
                if(bullet > 0)
            {
                Ray ray = cam.ScreenPointToRay(touch.position);
                RaycastHit hit;
                // Lanzamos un rayo desde la cámara hacia donde el jugador tocó

                //Empezamos la fase Began
                if (Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    Debug.Log("Pium Pium");

                    //Lanzamos un ray desde la dirección del dedo
                    //Ray ray = cam.ScreenPointToRay(touch.position);
                    //RaycastHit hit;
                    Debug.DrawRay(ray.origin, ray.direction * maxDistance, Color.red, 1f);
                    //Si el rayo choca con un objeto con la layer enemy, ~0 detecta todas las capas posibles
                    //Detecta colliders normales y triggers
                    
                    if (Physics.Raycast(ray, out hit, maxDistance, ~0, QueryTriggerInteraction.Collide))
                    {
                        string tagHit = hit.collider.gameObject.tag;
                        // Instanciar partículas si NO es un objeto de recarga
                        if (tagHit != "RecargaObjeto")
                        {
                            GameObject Particulas = Instantiate(SistemaParticulas, hit.point, Quaternion.identity);
                            StartCoroutine(SistemaDeParticulasCO(Particulas));
                        }
                        //Instantiate(SistemaParticulas, hit.transform.position, SistemaParticulas.transform.rotation);
                        //StartCoroutine(SistemaDeParticulasCO());
                        if (tagHit == "Enemigo")
                        {
                            bullet--;
                            GameObject AnimacionEnemigo = Instantiate(MuerteAnim, hit.point, Quaternion.identity);
                            StartCoroutine(AnimacionMuerteCo(AnimacionEnemigo));

                            //Obtenemos el Script Score y desactivamos el objeto
                            _scoreRef = hit.collider.gameObject.GetComponent<Score>();
                            hit.collider.gameObject.SetActive(false);
                            //SpawnEnemies spawner = FindObjectOfType<SpawnEnemies>();
                            EnemigosEliminados++;
                            if (combo != null)
                            {
                                int multiplicador = 1; // Por defecto sin combo
                                if (EnemigosEliminados >= 4)
                                {
                                    comboActivado = true;
                                    multiplicador = combo.AddKill(); // Solo ahora aplicamos multiplicador real
                                }
                                puntuacion += _scoreRef.score * multiplicador;
                            }

                            //puntuacion = puntuacion + (_scoreRef.score * multiplicador);
                            //if (spawner != null)
                            //{
                            //    spawner.AddToRespawnList(hit.collider.gameObject);
                            //}
                        }
                        if (tagHit == "Escenario")
                        {
                            bullet--;
                        }
                        if (tagHit == "RecargaObjeto")
                        {
                            Debug.Log("ObjetoRecarga");
                        }
                    }
                    else
                    {
                        bullet--;
                        GameObject Particulas =Instantiate(SistemaParticulas, ray.origin + ray.direction * 10f, Quaternion.identity);
                        StartCoroutine(SistemaDeParticulasCO(Particulas));
                        if (combo != null)
                            combo.FailShot();
                    }

                }
            }


        }
        if (bullet <= 0 && !cargaRapida && canShoot)
        {
            canShoot = false;
            cargaRapida = true;
        }
    }
    private IEnumerator SistemaDeParticulasCO(GameObject Particulas)
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(Particulas);
    }
    private IEnumerator AnimacionMuerteCo(GameObject AnimacionEnemigo)
    {
        yield return new WaitForSeconds(0.35f);
        Destroy(AnimacionEnemigo);
    }
    public void RecargaRapida()
    {
        if (cargaRapida == true)
        {
            Recarga_Text.SetActive(true);
            anim_Boton_Recarga.SetBool("Recargando", true);
            anim_Text_Recarga.SetBool("Recargando", true);
            cargaRapidaContador -= Time.deltaTime;
            Debug.Log("Recarga Rapida");
            if (cargaRapidaContador <= 0)
            {
                Debug.Log("Perdiste la carga rapida");
                cargaRapida = false;
                cargaRapidaContador = cargaRapidaTiempo;
                anim_Boton_Recarga.SetBool("Recargando", false);
                anim_Text_Recarga.SetBool("Recargando", false);

            }
            if (bullet <= 0 && cargaRapidaContador > 0 && SimpleInput.GetButtonDown(Recarga))
            {

                reloadContador = reloadTiempo;
                //reloadContador = 0;
                bullet = cargador;
                canShoot = true;
                cargaRapida = false;
                cargaRapidaContador = cargaRapidaTiempo;
                anim_Boton_Recarga.SetBool("Recargando", false);
                anim_Text_Recarga.SetBool("Recargando", false);
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
                Recarga_Text.SetActive(false);

            }
        }
    }
    private void Mostrartexto()
    {
        //El texto será igual a la puntuacion
        textScore.text = "Puntuación " + puntuacion.ToString();
        balaText.text = "6 / " + bullet.ToString();
    }
}
