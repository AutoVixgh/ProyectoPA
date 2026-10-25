using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuzSeguimientoMouse : MonoBehaviour
{
    private Camera mainCamera;
    void Start()
    {
        mainCamera = Camera.main;
    }
    void Update()
    {
        // Convierte la posición del mouse en coordenadas del mundo del juego
        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f; // Asegura que se mantenga en el plano 2D

        transform.position = mousePosition;
    }
}
