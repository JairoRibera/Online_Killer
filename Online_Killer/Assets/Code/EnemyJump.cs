using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyJump : MonoBehaviour
{
    public float distancia;
    public bool canJump;
    public GameObject[] points;
    public float jumpHeight = 3f;     // Altura máxima del salto
    public float jumpCooldown = 2f;   // Tiempo entre saltos
    public float gravity = 9.81f;     // Gravedad (igual que la del Rigidbody)
    public float jumpSpeedMultiplier = 1f; // Ajuste de velocidad horizontal
    public bool isGround = false;
    private Rigidbody rb;
    private Vector3 targetPoint;
    private bool isJumping = false;
    private Vector3 posHEnemy;
    private Vector3 posHPoint;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        points = GameObject.FindGameObjectsWithTag("RandomPoint");
        StartCoroutine(JumpRoutine());
    }

    IEnumerator JumpRoutine()
    {
        while (true)
        {
            if (!isJumping)
            {
                FindRandomPoint();
                JumpToTarget(targetPoint);
            }

            yield return new WaitForSeconds(jumpCooldown);
        }
    }

    void FindRandomPoint()
    {
        int randomIndex = Random.Range(0, points.Length);
        targetPoint = points[randomIndex].transform.position;

    }
    public void IsGround()
    {
        isGround = Physics.Raycast(transform.position, Vector3.down, 1.1f);
        if(isGround == true)
        {
            canJump = true;
        } 
        else canJump = false;
    }
    public void TimerDistance()
    {

    }
    public void CalcularDistancia()
    {
        if (isGround == true)
        {

        }
        posHEnemy = new Vector3(transform.position.x, 0, transform.position.z);
        posHPoint = new Vector3(targetPoint.x, 0, targetPoint.z);
        distancia = Vector3.Distance(posHEnemy, posHPoint);
        // Calcular la velocidad vertical necesaria para alcanzar la altura deseada
        float velocityY = Mathf.Sqrt(2 * gravity * jumpHeight);
        //transform.position = Vector3.MoveTowards(posHEnemy, posHPoint, Vector3.up * jumpHeight  * Time.deltaTime);
        // Calcular el tiempo total del salto (subida + bajada)
        float timeUp = velocityY / gravity;
        float totalTime = timeUp * 2;
        // Calcular la velocidad horizontal necesaria para llegar en ese tiempo
        ////Vector3 velocityXZ = displacementXZ / totalTime * jumpSpeedMultiplier;

        //// Combinar velocidad vertical + horizontal
        //Vector3 launchVelocity = velocityXZ + Vector3.up * velocityY;

        //// Aplicar el impulso
        //rb.velocity = launchVelocity;
    }
    void JumpToTarget(Vector3 target)
    {
        Vector3 startPos = transform.position;
        Vector3 targetPos = target;

        // Calcular distancia horizontal (XZ)
        Vector3 displacementXZ = new Vector3(targetPos.x - startPos.x, 0, targetPos.z - startPos.z);
        float distanceXZ = displacementXZ.magnitude;

        // Calcular la velocidad vertical necesaria para alcanzar la altura deseada
        float velocityY = Mathf.Sqrt(2 * gravity * jumpHeight);

        // Calcular el tiempo total del salto (subida + bajada)
        float timeUp = velocityY / gravity;
        float totalTime = timeUp * 2;

        // Calcular la velocidad horizontal necesaria para llegar en ese tiempo
        Vector3 velocityXZ = displacementXZ / totalTime * jumpSpeedMultiplier;

        // Combinar velocidad vertical + horizontal
        Vector3 launchVelocity = velocityXZ + Vector3.up * velocityY;

        // Aplicar el impulso
        rb.velocity = launchVelocity;

        isJumping = true;
        StartCoroutine(WaitForLanding());
    }

    IEnumerator WaitForLanding()
    {
        // Espera hasta que el enemigo toque el suelo de nuevo
        yield return new WaitUntil(() => rb.velocity.y <= 0 && IsGrounded());
        isJumping = false;
    }

    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 1.1f);
    }
}
