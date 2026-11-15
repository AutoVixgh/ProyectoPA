using UnityEngine;

public class HookshotAbility : MonoBehaviour
{
    private movimientoDelJugador jugador;
    private Rigidbody2D rb;
    private Animator anim;

    [SerializeField] private float distanciaGancho = 8f;
    [SerializeField] private LayerMask capaGancho;
    [SerializeField] private float fuerzaImpulsoGancho = 15f;

    [HideInInspector] public bool estaEnganchado = false;

    void Start()
    {
        jugador = GetComponent<movimientoDelJugador>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // Activo si el índice es 3 y presiona M
        if (jugador != null && jugador.activePowerIndex == 3 && Input.GetKeyDown(KeyCode.M))
        {
            LanzarGancho();
        }
    }

    void LanzarGancho()
    {
        Vector2 direccion = ObtenerDireccionGancho();

        Debug.DrawRay(transform.position, direccion * distanciaGancho, Color.cyan, 1.5f);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direccion, distanciaGancho, capaGancho);

        if (hit.collider != null)
        {
            estaEnganchado = true;

            if (anim != null)
            {
                anim.SetTrigger("TriggerHook");
            }

            // Impulso hacia el punto exacto de impacto
            Vector2 fuerzaImpulso = (hit.point - (Vector2)transform.position).normalized * fuerzaImpulsoGancho;
            rb.velocity = fuerzaImpulso;

            Invoke("SoltarGancho", 0.15f);
        }
    }

    Vector2 ObtenerDireccionGancho()
    {
        bool arriba = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
        bool izquierda = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);
        bool derecha = Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);

        if (arriba && izquierda)
        {
            return new Vector2(-1f, 1f).normalized;
        }

        if (arriba && derecha)
        {
            return new Vector2(1f, 1f).normalized;
        }

        if (arriba)
        {
            return Vector2.up;
        }

        bool mirandoIzquierda = jugador.GetComponent<SpriteRenderer>().flipX;
        return mirandoIzquierda ? Vector2.left : Vector2.right;
    }

    public void SoltarGancho()
    {
        estaEnganchado = false;
    }
}