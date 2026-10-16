using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vidaDelJugador : MonoBehaviour
{
    public int vida;
    public int vidaMaxima = 4;

    // sprite idle de la rata
    public SpriteRenderer playerSr;

    // script del movimiento del jugador
    public movimientoDelJugador playerMovement;

    // script que maneja la pantalla del game over
    public GameOver gameOver;

    // sonido que la rata reproduce al recibir danio
    public AudioSource audioSource;


    // Start is called before the first frame update
    void Start()
    {
        // la rata comienza la escena teniendo la vida maxima
        vida = vidaMaxima;    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // funcion en caso de que la rata reciba dano, amount es la cantidad de dano que le hara un obstaculo a la rata
    public void QuitarVida(int danio) {
        vida -= danio; 

        // la rata reproduce sonido al recibir dano
        if (audioSource != null) {
            audioSource.Play();
        }

        // si la cantidad de vidas es menor o igual a 0, quiere decir que la rata murio
        if (vida <= 0) {
            playerSr.enabled = false; // se desactiva el sprite de la rata
            playerMovement.enabled = false; // el script igual
            gameOver.ActivarGameOver(); // se procede a mostrar el panel de game over 
        }
    }
}
