using System;
using System.Collections;
using System.Collections.Generic;
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

    [Header("Estadísticas base de los Zombies")]

    public float vidaZ;
    public float velocidadZ;
    public float dañoZ;
    public float rangoZ;


    //public float vidaZR;
    /*
    [Serializable]
    public class Jugador
    {
        public float vida;
        public float velocidad;
        public float daño;
        public float municionRest;
        public float energíaRest;
    }
    
    [Serializable]
    public class Zombie
    {
        public float vida;
        public float velocidad;
        public float daño;
        public bool tieneArma;
    }
    
    public class ZRapido : Zombie
    {
        new public float velocidad; 
    }

    [SerializeField] private Jugador EstadísticasJugador;
    [SerializeField] private Zombie EstadísticasZombie;
    [SerializeField] private Zombie EstadísticasZRapido;
    */

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
