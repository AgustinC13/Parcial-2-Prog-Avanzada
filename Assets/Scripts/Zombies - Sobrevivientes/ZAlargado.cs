using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZAlargado : Zombie
{
    private void Start()
    {
        float multiplicadorVelocidad = 0.75f;

        agente.speed *= velocidadZ * multiplicadorVelocidad;
        velocidadOriginal = agente.speed;
        vidaZ *= 0.75f;
        rangoZ *= 1.75f;
        CooldownAtaque /= velocidadZ * multiplicadorVelocidad;
        animator.speed *= velocidadZ * multiplicadorVelocidad;
    }
}