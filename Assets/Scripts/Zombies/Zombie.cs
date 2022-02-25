using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour
{
    public NavMeshAgent agente;
    public Transform jugador;

    public Estadísticas est;

    [System.NonSerialized] public float vidaZ;
    [System.NonSerialized] public float velocidadZ;
    [System.NonSerialized] public float dañoZ;
    [System.NonSerialized] public float rangoZ;

    private void Awake()
    {
        vidaZ = est.vidaZ;
        velocidadZ = est.velocidadZ;
        dañoZ = est.dañoZ;
        rangoZ = est.rangoZ;

        agente.speed *= velocidadZ;
    }

    private void Update()
    {
        Vector3 gps = jugador.position;
        //float distanciaX = gameObject.transform.position.x - gps.x;
        //float distanciaY = gameObject.transform.position.y - gps.y;
        agente.SetDestination(gps);

        float distanciaXZ = Vector3.Distance(new Vector3(gps.x, 0, gps.z), new Vector3(gameObject.transform.position.x, 0, gameObject.transform.position.z));
        //float distanciaY = Vector3.Distance(new Vector3(0, gameObject.transform.position.y, 0), new Vector3(0, gps.y, 0));

        /*
        if (distanciaX > rangoZ && !Mathf.Approximately(distanciaY, 0))
        if (distanciaX > rangoZ && !Mathf.Approximately(distanciaY, 0))
        {
            Debug.Log(distanciaX);
            agente.isStopped = false;
        }
        else
        {
            Debug.Log("Cerca");
            agente.isStopped = true;
        }
        NavMeshHit navMeshHit;
        if (NavMesh.SamplePosition(agente.transform.position, out navMeshHit, 1f, NavMesh.AllAreas))
        {
            agente.isStopped = false;
            agente.SetDestination(gps);
            Debug.Log(navMeshHit.mask + " " + gameObject.name);
        }
        */

        // Ataque del zombie

        if (distanciaXZ <= rangoZ)
        {
            est.vidaJ -= est.dañoZ;
            agente.isStopped = true;
        }
        else
        {
            agente.isStopped = false;
        }

        //Debug.Log("Estadísticas Zombie Normal: " + vidaZ + " " + velocidadZ + " " + dañoZ + " " + tieneArmaZ);
    }
}
