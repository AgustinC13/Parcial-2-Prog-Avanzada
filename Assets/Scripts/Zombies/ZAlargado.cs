using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZAlargado : Zombie
{
    private void Start()
    {
        agente.speed *= velocidadZ * 0.75f;
        vidaZ *= 0.75f;
        rangoZ *= 1;
    }
}