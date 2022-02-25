using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jugador : MonoBehaviour
{
    public CharacterController CharCont;
    public Camera Camara;

    private float velocidadInicial;
    private float energíaInicial;
    public float gravedad = -9.81f;
    public Vector3 direccion;

    public Transform checkPiso;
    public float distanciaPiso = 0.4f;
    public LayerMask capaPiso;
    public LayerMask capaEnemigos;
    bool tocaPiso;

    public Estadísticas est;

    private float segundosCooldownEnergía = 0;
    private float segundosCooldownDisparo = 0;

    private void Start()
    {
        velocidadInicial = est.velocidadJ;
        energíaInicial = est.energíaJ;
    }

    void Update()
    {
        // Movimiento

        tocaPiso = Physics.CheckSphere(checkPiso.position, distanciaPiso, capaPiso);

        if (tocaPiso && direccion.y < 0)
        {
            direccion.y = -2f;
        }

        /*
        if (Input.GetButtonDown("Jump") && tocaPiso)
        {
            direccion.y = Mathf.Sqrt(3 * -2 * gravedad);
        }*/

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 mover = transform.right * x + transform.forward * z;
        CharCont.Move(mover * est.velocidadJ * Time.deltaTime);


        direccion.y += gravedad * Time.deltaTime;
        CharCont.Move(direccion * Time.deltaTime);

        // Correr

        segundosCooldownEnergía += Time.deltaTime;

        if (Input.GetKey(KeyCode.LeftShift) && est.energíaJ > 0)
        {
            est.velocidadJ = velocidadInicial * 2;
            segundosCooldownEnergía = 0;
            est.energíaJ -= .33f;
        }
        else
        {
            est.velocidadJ = velocidadInicial;

            if (segundosCooldownEnergía >= est.cooldownEnergíaJ)
            {
                est.energíaJ += .175f;
            }
        }

        // Limitar estadísticas

        if (est.vidaJ <= 0)
        {
            est.vidaJ = 0;
        }

        if (est.energíaJ > energíaInicial)
        {
            est.energíaJ = energíaInicial;
        }

        // Raycast (Disparo)

        RaycastHit hit;

        segundosCooldownDisparo += Time.deltaTime;

        if (Input.GetMouseButtonDown(0) && segundosCooldownDisparo >= est.cooldownAtaqueJ)
        {
            segundosCooldownDisparo = 0;

            if (Physics.Raycast(Camara.gameObject.transform.position, Camara.gameObject.transform.TransformDirection(Vector3.forward), out hit, est.rangoJ, capaEnemigos))
            {
                Debug.DrawRay(Camara.gameObject.transform.position, Camara.gameObject.transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                Debug.Log("Golpeo enemigo");

                hit.transform.GetComponent<Zombie>().vidaZ -= est.dañoJ;

                if (hit.transform.GetComponent<Zombie>().vidaZ <= 0)
                {
                    Destroy(hit.transform.gameObject);
                }
            }
            else
            {
                Debug.DrawRay(Camara.gameObject.transform.position, Camara.gameObject.transform.TransformDirection(Vector3.forward) * 1000, Color.white);
                Debug.Log("Golpeo otra cosa");
            }
        }
    }
}