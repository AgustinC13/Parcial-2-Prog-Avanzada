using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Jugador : MonoBehaviour
{
    public CharacterController CharCont;
    public Camera Camara;
    public Animator animator;

    public float gravedad = -9.81f;
    public Vector3 direccion;

    public Transform checkPiso;
    public float distanciaPiso = 0.4f;
    public LayerMask capaPiso;
    public LayerMask capaEnemigos;
    bool tocaPiso;

    public Estadísticas est;

    private float velocidadInicial;
    private float energíaInicial;
    private float municionInicial;

    private bool puedeRecargar = true;
    [System.NonSerialized] public bool rescatando = false;

    private float segundosCooldownEnergía = 0;
    private float segundosCooldownDisparo = 0;

    public GameObject menuPausa;

    private void Start()
    {
        velocidadInicial = est.velocidadJ;
        energíaInicial = est.energíaJ;
        municionInicial = est.municionJ;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // Movimiento

            // Saber si toca el piso o no

            tocaPiso = Physics.CheckSphere(checkPiso.position, distanciaPiso, capaPiso);

            if (tocaPiso && direccion.y < 0)
            {
                direccion.y = -2f;
            }

            // Salto

            /*if (Input.GetButtonDown("Jump") && tocaPiso)
            {
                direccion.y = Mathf.Sqrt(3 * -2 * gravedad);
            }*/

            // Movimiento basico - Caminar

            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 mover = transform.right * x + transform.forward * z;
            CharCont.Move(mover * est.velocidadJ * Time.deltaTime);

            direccion.y += gravedad * Time.deltaTime;
            CharCont.Move(direccion * Time.deltaTime);

                // Animaciones

                if ((x != 0 || z != 0) && est.velocidadJ == 3 && animator.GetInteger("SUPERESTADO") != 3)
                {
                    animator.SetInteger("SUPERESTADO", 1);
                }
                else if (x == 0 && z == 0 && animator.GetInteger("SUPERESTADO") != 3)
                {
                    animator.SetInteger("SUPERESTADO", 0);
                }

            // Correr

            segundosCooldownEnergía += Time.deltaTime;

            if (Input.GetKey(KeyCode.LeftShift) && est.energíaJ > 0 && animator.GetInteger("SUPERESTADO") != 3)
            {
                est.velocidadJ = velocidadInicial * 2;
                segundosCooldownEnergía = 0;
                est.energíaJ -= .33f;
            
                animator.SetInteger("SUPERESTADO", 2);
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

        if (Input.GetMouseButtonDown(0) && segundosCooldownDisparo >= est.cooldownAtaqueJ && est.municionJ > 0 && puedeRecargar == true)
        {
            segundosCooldownDisparo = 0;

            est.municionJ -= 1;

            animator.SetInteger("SUPERESTADO", 3);

            CharCont.enabled = false;
            this.gameObject.GetComponent<Rigidbody>().isKinematic = true;

            if (Physics.Raycast(Camara.gameObject.transform.position, Camara.gameObject.transform.TransformDirection(Vector3.forward), out hit, est.rangoJ, capaEnemigos))
            {
                Debug.DrawRay(Camara.gameObject.transform.position, Camara.gameObject.transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                Debug.Log("Golpeo enemigo");

                hit.transform.GetComponent<Zombie>().vidaZ -= est.dañoJ;

                if (hit.transform.GetComponent<Zombie>().vidaZ <= 0)
                {
                    est.zombiesVivos -= 1;
                    Destroy(hit.transform.gameObject);
                }
            }
            else
            {
                Debug.DrawRay(Camara.gameObject.transform.position, Camara.gameObject.transform.TransformDirection(Vector3.forward) * 1000, Color.white);
                Debug.Log("Golpeo otra cosa");
            }
        }

        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
        {
            CharCont.enabled = true;
            this.gameObject.GetComponent<Rigidbody>().isKinematic = false;

            animator.SetInteger("SUPERESTADO", 0);
        }

        // Recargar munición

        if (Input.GetKeyDown(KeyCode.R) && puedeRecargar && animator.GetInteger("SUPERESTADO") != 3)
        {
            puedeRecargar = false;

            CharCont.enabled = false;
            this.gameObject.GetComponent<Rigidbody>().isKinematic = true;

            StartCoroutine(Recarga());
        }

        // Pausar el juego

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            MenuPausaDerrotaVictoria();

            menuPausa.transform.GetChild(0).GetComponent<Text>().text = "Pausa";
        }

        // Perder

        if (est.vidaJ <= 0)
        {
            MenuPausaDerrotaVictoria();

            menuPausa.transform.GetChild(0).GetComponent<Text>().text = "Te han matado...";

            eliminarDespausar();
        }
        else if (est.tiempoRestante <= 0)
        {
            MenuPausaDerrotaVictoria();

            menuPausa.transform.GetChild(0).GetComponent<Text>().text = "Se acabó el tiempo";

            eliminarDespausar();
        }

        // Ganar

        if (est.sobrevivientesRestantes == 0)
        {
            MenuPausaDerrotaVictoria();

            menuPausa.transform.GetChild(0).GetComponent<Text>().text = "¡Has ganado!";

            eliminarDespausar();
        }
    }

    // Menu al pausar o perder

    void MenuPausaDerrotaVictoria()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        menuPausa.SetActive(true);

        Time.timeScale = 0;

        this.enabled = false;
    }

    // Eliminar el boton de despausar para los menús de derrota y victoria

    void eliminarDespausar()
    {
        menuPausa.transform.GetChild(1).gameObject.SetActive(false);
        menuPausa.transform.GetChild(2).GetComponent<RectTransform>().position = new Vector2(960, 540);
        menuPausa.transform.GetChild(3).GetComponent<RectTransform>().position = new Vector2(960, 360);
    }

    // Esperar al recargar municion

    IEnumerator Recarga()
    {
        yield return new WaitForSeconds(est.tiempoRecargaJ);

        puedeRecargar = true;

        CharCont.enabled = true;
        this.gameObject.GetComponent<Rigidbody>().isKinematic = false;

        est.municionJ = municionInicial;
    }
}