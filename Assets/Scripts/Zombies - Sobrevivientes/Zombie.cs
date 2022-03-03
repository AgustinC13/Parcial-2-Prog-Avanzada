using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour
{
    public NavMeshAgent agente;
    public Animator animator;
    public FieldOfView fovLejos;
    public FieldOfView fovCerca;
    public FieldOfView fovJugDetectado;

    private Transform jugador;
    private Estadísticas est;

    [System.NonSerialized] public float vidaZ;
    [System.NonSerialized] public float velocidadZ;
    [System.NonSerialized] public float dañoZ;
    [System.NonSerialized] public float rangoZ;
    [System.NonSerialized] public float CooldownAtaque;
    [System.NonSerialized] public float velocidadOriginal;
    [System.NonSerialized] public float segundosCooldownAtaque;

    private void Awake()
    {
        jugador = GameObject.Find("Jugador").GetComponent<Transform>();
        est = GameObject.Find("Game Manager").GetComponent<Estadísticas>();

        vidaZ = est.vidaZ;
        velocidadZ = est.velocidadZ;
        dañoZ = est.dañoZ;
        rangoZ = est.rangoZ;
        CooldownAtaque = est.cooldownAtaqueZ;

        agente.speed *= velocidadZ;
        velocidadOriginal = agente.speed;

        fovJugDetectado.enabled = false;
    }

    private void Update()
    {
        // Destino del zombie

        agente.SetDestination(jugador.position);

        // Distancia entre el jugador y el zombie

        float distancia = Vector3.Distance(jugador.position, gameObject.transform.position);

        // Estado del zombie

        segundosCooldownAtaque += Time.deltaTime;

            // Quieto

            if (distancia <= rangoZ)
            {
                agente.speed = 0;
            }

                // Sin ver al jugador

                if (fovCerca.canSeePlayer == false && fovLejos.canSeePlayer == false && fovJugDetectado.canSeePlayer == false)
                {
                    agente.isStopped = true;
                    animator.SetInteger("SUPERESTADO", 0);

                    fovJugDetectado.enabled = false;
                }

                // Habiendo visto al jugador

                if (distancia <= rangoZ && (fovCerca.canSeePlayer == true || fovLejos.canSeePlayer == true || fovJugDetectado == true))
                {
                    animator.SetInteger("SUPERESTADO", 2);
                    
                    agente.isStopped = true;

                    //transform.LookAt(jugador);

                    if (segundosCooldownAtaque >= CooldownAtaque)
                    {
                        segundosCooldownAtaque = 0;

                        if (est.vidaJ >= 0)
                        {
                            est.vidaJ -= est.dañoZ;
                        }
                    }
                }

            // En movimiento

            if (distancia > rangoZ && (fovCerca.canSeePlayer == true || fovLejos.canSeePlayer == true || fovJugDetectado.canSeePlayer == true))
            {
                agente.isStopped = false;
                agente.speed = velocidadOriginal;

                fovJugDetectado.enabled = true;

                animator.SetInteger("SUPERESTADO", 1);
            }
    }
}
