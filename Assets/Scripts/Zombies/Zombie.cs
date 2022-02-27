using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour
{
    public NavMeshAgent agente;
    public Transform jugador;
    public Animator animator;

    public Estadísticas est;
    public FieldOfView fovLejos;
    public FieldOfView fovCerca;
    public FieldOfView fovJugDetectado;

    [System.NonSerialized] public float vidaZ;
    [System.NonSerialized] public float velocidadZ;
    [System.NonSerialized] public float dañoZ;
    [System.NonSerialized] public float rangoZ;

    private float segundosCooldownAtaque;
    private float velocidadOriginal;

    private void Awake()
    {
        vidaZ = est.vidaZ;
        velocidadZ = est.velocidadZ;
        dañoZ = est.dañoZ;
        rangoZ = est.rangoZ;

        agente.speed *= velocidadZ;
        velocidadOriginal = agente.speed;
    }

    private void Update()
    {
        // Dirección del zombie

        Vector3 gps = jugador.position;

        agente.SetDestination(gps);

        float distancia = Vector3.Distance(gps, gameObject.transform.position);



        // Ataque del zombie

        segundosCooldownAtaque += Time.deltaTime;

        if (fovCerca.canSeePlayer == false && fovLejos.canSeePlayer == false && fovJugDetectado.canSeePlayer == false)
        {
            //agente.speed = velocidadOriginal;
            agente.isStopped = true;
            fovJugDetectado.enabled = false;
        }


        if (distancia <= rangoZ && segundosCooldownAtaque >= est.cooldownAtaqueZ && (fovCerca.canSeePlayer == true || fovLejos.canSeePlayer == true || fovJugDetectado == true))
        {
            segundosCooldownAtaque = 0;

            if (est.vidaJ >= 0)
            {

                est.vidaJ -= est.dañoZ;

            }

             animator.SetInteger("SUPERESTADO", 2);

        }
        else //if (fovCerca.canSeePlayer == true || fovLejos.canSeePlayer == true)
        {
            agente.isStopped = false;
            agente.speed = velocidadOriginal;
            fovJugDetectado.enabled = true;


            if (distancia > rangoZ)
            {
                animator.SetInteger("SUPERESTADO", 1);
            }


        }

        if (distancia <= rangoZ)
        {
            agente.speed = 0;
        }

        //Debug.Log("Estadísticas Zombie Normal: " + vidaZ + " " + velocidadZ + " " + dañoZ + " " + tieneArmaZ);
    }
}
