using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZGordo : Zombie
{
    private void Start()
    {
        agente.speed *= velocidadZ * 0.75f;
        vidaZ *= 2f;
    }
}