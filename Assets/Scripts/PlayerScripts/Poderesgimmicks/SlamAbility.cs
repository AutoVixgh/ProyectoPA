using UnityEngine;

public class SlamAbility : MonoBehaviour
{
    private movimientoDelJugador jugador;
    private Rigidbody2D rb;
    [SerializeField] private float fuerzaSlam = 18f;
    private bool estaHaciendoSlam = false;

    void Start()
    {
        jugador = GetComponent<movimientoDelJugador>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Activo si el índice es 1, está en el aire y presiona M
        if (jugador != null && jugador.activePowerIndex == 1 && !jugador.jugadorEstaTocandoSuelo && Input.GetKeyDown(KeyCode.M))
        {
            EjecutarSlam();
        }
    }

    void EjecutarSlam()
    {
        estaHaciendoSlam = true;
        rb.velocity = new Vector2(0f, -fuerzaSlam);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (estaHaciendoSlam && jugador.jugadorEstaTocandoSuelo)
        {
            estaHaciendoSlam = false;

            // Si choca con un objeto frágil, lo destruye
            
            //if (collision.gameObject.CompareTag("Fragil"))
            //{
                //Destroy(collision.gameObject);
            //}
            
        }
    }
}