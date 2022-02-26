using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovCamara : MonoBehaviour
{
    public float sensibilidad = 100;
    public Transform Jugador;
    public float rotacionX;

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensibilidad * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensibilidad * Time.deltaTime;

        rotacionX -= mouseY;
        rotacionX = Mathf.Clamp(rotacionX, -90, 90);

        transform.localRotation = Quaternion.Euler(rotacionX, 0, 0);

        Jugador.Rotate(Vector3.up * mouseX);
    }
}
