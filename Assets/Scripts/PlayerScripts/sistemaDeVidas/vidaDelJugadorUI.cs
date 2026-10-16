using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class vidaDelJugadorUI : MonoBehaviour
{
    // vida actual del jugador
    public int vida;

    // vida maxima
    public int vidaMaxima;

    public Sprite corazonVacio;
    public Sprite corazonLleno;

    // array que almacena la cantidad x de vidas que le asignamos al jugador 
    public Image[] ArrayDeVidas;

    // referencia al script que maneja la vida del jugador
    public vidaDelJugador vidaDelJugador;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //en la pantalla, se mostrara la cantidad x de corazones dependiendo de la vida que le hayamos asignado al jugador
        if (vidaDelJugador == null) return;

        // vinculamos las variables del script que maneja la logica del sistema de vidas
        vida = vidaDelJugador.vida;
        vidaMaxima = vidaDelJugador.vidaMaxima;

        // Recorremos todos los corazones configurados en el array de la UI
        for (int i = 0; i < ArrayDeVidas.Length; i++) 
        {
            if (i < vidaMaxima)
            {
                ArrayDeVidas[i].enabled = true; 

                if (i < vida)
                {
                    ArrayDeVidas[i].sprite = corazonLleno;
                }
                else
                {
                    ArrayDeVidas[i].sprite = corazonVacio;
                }
            }
            else
            {
                ArrayDeVidas[i].enabled = false;
            }
        }
    }
}
