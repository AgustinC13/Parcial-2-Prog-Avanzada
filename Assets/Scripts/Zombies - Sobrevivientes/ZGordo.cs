using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZGordo : Zombie
{
    private void Start()
    {
        float multiplicadorVelocidad = 0.75f;

        agente.speed *= velocidadZ * multiplicadorVelocidad;
        velocidadOriginal = agente.speed;
        vidaZ *= 2f;
        rangoZ *= 1.4f;
        CooldownAtaque /= velocidadZ * multiplicadorVelocidad;
        animator.speed *= velocidadZ * multiplicadorVelocidad;
    }
}