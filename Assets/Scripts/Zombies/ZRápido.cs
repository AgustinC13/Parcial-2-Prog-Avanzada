using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZRápido : Zombie
{
    private void Start()
    {
        agente.speed *= velocidadZ * 1.425f;
        vidaZ *= .5f;
    }
}