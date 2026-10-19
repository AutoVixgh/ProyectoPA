using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movimientoDelJugador : MonoBehaviour
{
    [SerializeField] private float velocidadDeMovimientoEjeX = 5f;
    [SerializeField] private float fuerzaDelSalto = 5f;

    // desde el jugador sale una linea vertical que apunta a una distancia x hacia abajo, esa distancia es un detector de suelo para el jugador. (raycast)
    [SerializeField] private float groundCheckDistance = 0.5f;

    // se puede seleccionar un tipo de capa en especifico que nuestro raycast detecte, en este caso ground 
    [SerializeField] private LayerMask groundLayer;

    // Componentes y variables internas
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    private float velocidadActualEjeX;
    private bool jugadorEstaTocandoSuelo = true;
    private bool jugadorEstaSaltando = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        movimientoDelJugadorEjeX();
        saltoDelJugador();
    }

    void FixedUpdate()
    {
        detectorDeSuelo();
        aplicarMovimiento();
    }

    // salto del jugador
    private void saltoDelJugador()
    {
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)) && jugadorEstaTocandoSuelo)
    {
        jugadorEstaSaltando = true;
    }
    }

    // funcion del movimiento horizontal del personaje
    public void movimientoDelJugadorEjeX()
    {
        velocidadActualEjeX = 0f;
        
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) velocidadActualEjeX -= 1f;
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) velocidadActualEjeX += 1f;

        if (velocidadActualEjeX > 0)
        {
            spriteRenderer.flipX = false; 
        }
        else if (velocidadActualEjeX < 0)
        {
            spriteRenderer.flipX = true;  
        }
    } 

    private void aplicarMovimiento()
    {
        float velocidadActualEjeY = rb.velocity.y;

        if (jugadorEstaSaltando)
        {
            velocidadActualEjeY = fuerzaDelSalto;
            jugadorEstaSaltando = false;
        }

        rb.velocity = new Vector2(velocidadActualEjeX * velocidadDeMovimientoEjeX, velocidadActualEjeY);
    }


    // permite saber si el jugador esta tocando el suelo mediante un rayo "raycast" que sale de la posicion actual hacia abajo
    private void detectorDeSuelo()
    {
        RaycastHit2D hit = Physics2D.Raycast(
            transform.position,
            Vector2.down,
            groundCheckDistance,
            groundLayer
        );

        jugadorEstaTocandoSuelo = hit.collider != null;

        Debug.DrawRay(transform.position, Vector2.down * groundCheckDistance, jugadorEstaTocandoSuelo ? Color.green : Color.red);
    }
}
