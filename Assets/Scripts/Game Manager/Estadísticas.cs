using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
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

    [Header("Estadísticas base de los Zombies")]

    public float vidaZ;
    public float velocidadZ;
    public float dañoZ;
    public float rangoZ;
    public float cooldownAtaqueZ;

    [System.NonSerialized] public int zombiesVivos;

    [Header("Estadísticas base de los Sobrevivientes")]
    public float velocidadS;
    public float rangoS;

    [System.NonSerialized] public int sobrevivientesRestantes;

    [Header("Textos del HUD")]
    public Text textoVidaJ;
    public Text textoEnergiaJ;
    public Text textoZombVivos;
    public Text textoSobRest;

    private void Start()
    {
        foreach (object sobreviviente in UnityEngine.Object.FindObjectsOfType<Sobreviviente>())
        {
            sobrevivientesRestantes += 1;
        }

        foreach (object zombies in UnityEngine.Object.FindObjectsOfType<Zombie>())
        {
            zombiesVivos += 1;
        }
    }

    void Update()
    {
        // Actualizar salud en el HUD
        
        textoVidaJ.text = "Salud: " + vidaJ.ToString();

        // Actualizar estamina en HUD

        textoEnergiaJ.text = "Estamina: " + Mathf.Round(energíaJ).ToString();

        // Actualizar sobrevivientes restantes en el HUD

        textoSobRest.text = "Sobrevivientes: " + sobrevivientesRestantes.ToString() + "/5";

        // Actualizar zombies vivos en el HUD

        textoZombVivos.text = "Zombies vivos: " + zombiesVivos.ToString();
    }
}
