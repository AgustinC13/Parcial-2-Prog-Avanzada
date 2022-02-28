using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Sobreviviente : MonoBehaviour
{
    public NavMeshAgent agente;
    public Transform jugador;
    public Animator animator;

    public Estadísticas est;

    [System.NonSerialized] public float velocidadS;
    [System.NonSerialized] public float rangoS;

    private float velocidadOriginal;
    private bool siendoRescatado;

    public void Awake()
    {
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

                animator.SetInteger("SUPERESTADO", 0);
            }

            // En movimiento

            if (distancia <= rangoS && Input.GetKeyDown(KeyCode.E))
            {
                siendoRescatado = true;

              //  animator.SetInteger("SUPERESTADO", 1);

        }

            if (siendoRescatado == true && distancia > rangoS)
            {
                agente.isStopped = false;
                agente.speed = velocidadOriginal;

               animator.SetInteger("SUPERESTADO", 1);
            }

            jugador.GetComponent<Jugador>().segundosCooldownEnergía += Time.deltaTime;

            if (Input.GetKey(KeyCode.LeftShift) && est.energíaJ > 0)
            {
                velocidadS = velocidadOriginal * 2;
            
               // animator.SetInteger("SUPERESTADO", 2);
            }
            else
            {
                velocidadS = velocidadOriginal;
            }
    }
}