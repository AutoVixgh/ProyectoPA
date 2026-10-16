using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProvocarDanio : MonoBehaviour
{
    // se referencia al script que maneja la vida del jugador
    public vidaDelJugador VidaDelJugador;

    // que tanto danio hara este gameobject a nuestra rata
    public int Danio = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // cuando exista una colision entre este gameobject que contenga este script de hacer danio y el jugador, la rata recibe el danio establecido 
    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.tag == "Player") {
            VidaDelJugador.QuitarVida(Danio);
        }
    }
}
