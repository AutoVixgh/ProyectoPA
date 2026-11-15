using UnityEngine;
using System.Collections;

public class AirDashAbility : MonoBehaviour
{
    private movimientoDelJugador jugador;
    private Rigidbody2D rb;
    [SerializeField] private float velocidadDash = 15f;
    [SerializeField] private float duracionDash = 0.2f;
    private bool puedeDash = true;
    [HideInInspector] public bool haciendoDash = false;

    void Start()
    {
        jugador = GetComponent<movimientoDelJugador>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (jugador.jugadorEstaTocandoSuelo)
        {
            puedeDash = true;
        }

        // Activo si el índice es 2 (Air Dash), está en el aire, puede hacer dash y presiona M
        if (jugador != null && jugador.activePowerIndex == 2 && !jugador.jugadorEstaTocandoSuelo && puedeDash && Input.GetKeyDown(KeyCode.M))
        {
            StartCoroutine(EjecutarDash());
        }
    }

    IEnumerator EjecutarDash()
    {
        haciendoDash = true;
        puedeDash = false;

        float gravedadOriginal = rb.gravityScale;
        rb.gravityScale = 0f; 

        // Determina donde mira la rata
        float direccion = jugador.GetComponent<SpriteRenderer>().flipX ? -1f : 1f;

        rb.velocity = new Vector2(direccion * velocidadDash, 0f);

        yield return new WaitForSeconds(duracionDash);

        rb.gravityScale = gravedadOriginal; // Restaura la gravedad
        haciendoDash = false;
    }
}