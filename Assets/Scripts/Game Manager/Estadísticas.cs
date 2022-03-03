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
    public float energíaJ;
    public float cooldownAtaqueJ;
    public float cooldownEnergíaJ;
    public float tiempoRecargaJ;

    public float tiempoRestante;

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
    public Text textoMunicionJ;
    public Text textoZombVivos;
    public Text textoSobRest;
    public Text textoTiempoRest;

    private void Start()
    {
        // Añadir cada sobreviviente a su contador

        foreach (object sobreviviente in UnityEngine.Object.FindObjectsOfType<Sobreviviente>())
        {
            sobrevivientesRestantes += 1;
        }

        // Añadir cada zombie a su contador

        foreach (object zombies in UnityEngine.Object.FindObjectsOfType<Zombie>())
        {
            zombiesVivos += 1;
        }

        // Multiplicar el tiempo por 60 para que de en minutos

        tiempoRestante *= 60;
    }

    void Update()
    {
        // Actualizar la salud en el HUD
        
        textoVidaJ.text = "Salud: " + vidaJ.ToString();

        // Actualizar la estamina en el HUD

        textoEnergiaJ.text = "Estamina: " + Mathf.Round(energíaJ).ToString();

        // Actualizar la munición restante en el HUD

        textoMunicionJ.text = "Munción: " + municionJ + "/8";

        // Actualizar los sobrevivientes restantes en el HUD

        textoSobRest.text = "Sobrevivientes: " + sobrevivientesRestantes.ToString() + "/5";

        // Actualizar los zombies vivos en el HUD

        textoZombVivos.text = "Zombies vivos: " + zombiesVivos.ToString();

        // Actualizar el tiempo restante en el HUD

        tiempoRestante -= Time.deltaTime;
        Temporizador(tiempoRestante);
    }

    // Temporizador

    void Temporizador(float tiempoHUD)
    {
        tiempoHUD += 1;
        float minutos = Mathf.FloorToInt(tiempoHUD / 60);
        float segundos = Mathf.FloorToInt(tiempoHUD % 60);

        textoTiempoRest.text = "Tiempo: " + string.Format("{0:00}:{1:00}", minutos, segundos);
    }
}
