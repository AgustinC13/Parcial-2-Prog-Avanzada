using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class BotonesUI : MonoBehaviour
{
    public void Despausar()
    {
        Time.timeScale = 1;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        GameObject.Find("Jugador").GetComponent<Jugador>().enabled = true;

        GameObject.Find("Jugador").GetComponent<Jugador>().menuPausa.SetActive(false);
    }

    public void Jugar()
    {
        Time.timeScale = 1;

        SceneManager.LoadScene("Juego");
    }

    public void Menu()
    {
        Time.timeScale = 1;

        SceneManager.LoadScene("Menu");
    }

    public void Salir()
    {
        Time.timeScale = 1;

        Application.Quit();
    }
}
