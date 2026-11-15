using UnityEngine;

public class SuperJumpAbility : MonoBehaviour
{
    private movimientoDelJugador jugador;
    private Rigidbody2D rb;
    [SerializeField] private float fuerzaSuperSalto = 14f;

    void Start()
    {
        jugador = GetComponent<movimientoDelJugador>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Activo si el índice es 4, está en el suelo y presiona M
        if (jugador != null && jugador.activePowerIndex == 4 && jugador.jugadorEstaTocandoSuelo && Input.GetKeyDown(KeyCode.M))
        {
            EjecutarSuperSalto();
        }
    }

    void EjecutarSuperSalto()
    {
        rb.velocity = new Vector2(rb.velocity.x, fuerzaSuperSalto);
    }
}