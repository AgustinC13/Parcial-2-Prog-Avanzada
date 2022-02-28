using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Estadísticas : MonoBehaviour
{
    [Header("Estadísticas del Jugador")]

    public float vidaJ;
    public float velocidadJ;
    public float dañoJ;
    public float rangoJ;
    public float municionJ;
    public float cooldownAtaqueJ;
    public float energíaJ;
    public float cooldownEnergíaJ;

    public Text textoVidaJ;
    public Text textoEnergiaJ;

    [Header("Estadísticas base de los Zombies")]

    public float vidaZ;
    public float velocidadZ;
    public float dañoZ;
    public float rangoZ;
    public float cooldownAtaqueZ;

    [Header("Estadísticas base de los Sobrevivientes")]
    public float velocidadS;
    public float rangoS;

    void Start()
    {
        
    }

    void Update()
    {
        // Actualizar salud en HUD
        
        textoVidaJ.text = "Salud: " + vidaJ.ToString();

        // Actualizar estamina en HUD

        textoEnergiaJ.text = "Estamina: " + Mathf.Round(energíaJ).ToString();
    }
}
