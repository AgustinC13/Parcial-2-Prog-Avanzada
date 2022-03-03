using UnityEngine;
using UnityEngine.AI;

public class Sobreviviente : MonoBehaviour
{
    public NavMeshAgent agente;
    public Animator animator;

    private Transform jugador;
    private Estadísticas est;
    private Jugador scriptJugador;

    [System.NonSerialized] public float velocidadS;
    [System.NonSerialized] public float rangoS;

    private float velocidadOriginal;
    private bool siendoRescatado;

    public void Awake()
    {
        jugador = GameObject.Find("Jugador").GetComponent<Transform>();
        est = GameObject.Find("Game Manager").GetComponent<Estadísticas>();
        scriptJugador = jugador.gameObject.GetComponent<Jugador>();

        velocidadS = est.velocidadS;
        rangoS = est.rangoS;

        agente.speed *= velocidadS;
        velocidadOriginal = agente.speed;

        agente.isStopped = true;
        animator.SetInteger("SUPERESTADO", 0);

        agente.speed = 0;
    }

    void Update()
    {
        // Direccion del sobreviviente

        agente.SetDestination(jugador.position);

        // Distancia entre el jugador y el sobreviviente

        float distancia = Vector3.Distance(jugador.position, gameObject.transform.position);

        // Estado del sobreviviente

            // Quieto

            if (distancia <= rangoS)
            {
                agente.isStopped = true;
                agente.speed = 0;

                //transform.LookAt(jugador);

                animator.SetInteger("SUPERESTADO", 0);
            }

            // En movimiento

            if (distancia <= rangoS && Input.GetKeyDown(KeyCode.E) && scriptJugador.rescatando == false)
            {
                siendoRescatado = true;

                jugador.gameObject.GetComponent<Jugador>().rescatando = true;
            }

            if (siendoRescatado == true && distancia > rangoS)
            {
                agente.isStopped = false;
                agente.speed = velocidadOriginal;

                animator.SetInteger("SUPERESTADO", 1);
            }
    }

    // Al entrar en contacto con un trigger

    private void OnTriggerEnter(Collider other)
    {
        // Cuando el trigger es la salida

        if (other.gameObject.layer == 9)
        {
            scriptJugador.rescatando = false;
            est.sobrevivientesRestantes -= 1;
            Destroy(gameObject);
        }
    }
}