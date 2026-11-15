using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movimientoDelJugador : MonoBehaviour
{
    [Header("Movimiento Base")]
    [SerializeField] private float velocidadDeMovimientoEjeX = 5f;
    [SerializeField] private float velocidadAgachadoEjeX = 2.5f;
    [SerializeField] private float fuerzaDelSalto = 5f;

    [Header("Detección de Suelo")]
    [SerializeField] private float groundCheckDistance = 0.5f;
    [SerializeField] private LayerMask groundLayer;

    [Header("Sistema de Taunt (Pizza Tower style)")]
    [SerializeField] private AudioClip tauntSound;
    [SerializeField] private float tauntDuration = 0.4f;
    [SerializeField] private SpriteRenderer tauntImageRenderer;
    [SerializeField] private Sprite[] tauntSprites;

    // Componentes y variables internas
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private AudioSource audioSource;
    private AirDashAbility dashScript; 

    private float velocidadActualEjeX;
    [HideInInspector] public bool jugadorEstaTocandoSuelo = true;
    private bool jugadorEstaSaltando = false;
    private bool estaAgachado = false;
    private bool estaHaciendoTaunt = false;

    // Estado de Habilidades (Gimmicks)
    [HideInInspector] public int activePowerIndex = 0; // 0 = Base, 1 al 5 = Gimmicks

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        dashScript = GetComponent<AirDashAbility>(); 

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        if (tauntImageRenderer != null)
        {
            tauntImageRenderer.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (estaHaciendoTaunt) return; // Si hace haciendo el taunt, bloquea el resto de inputs

        movimientoDelJugadorEjeX();
        saltoDelJugador();
        agacharseInput();
        tauntInput();
    }

    void FixedUpdate()
    {
        detectorDeSuelo();

        if (estaHaciendoTaunt) return;

        if (dashScript != null && dashScript.haciendoDash)
        {
            return;
        }
        HookshotAbility hookScript = GetComponent<HookshotAbility>();
        if (hookScript != null && hookScript.estaEnganchado)
        {
            return; // Pausa el movimiento normal mientras el gancho te impulsa
        }

        aplicarMovimiento();
        actualizarAnimator();
    }

    // Salto del jugador
    private void saltoDelJugador()
    {
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.N))
            && jugadorEstaTocandoSuelo && !estaAgachado)
        {
            jugadorEstaSaltando = true;
        }
    }

    // Mecánica de agacharse con la tecla S o Flecha Abajo
    private void agacharseInput()
    {
        estaAgachado = (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) && jugadorEstaTocandoSuelo;
    }

    // Taunt con L
    private void tauntInput()
    {
        if (Input.GetKeyDown(KeyCode.L) && !estaHaciendoTaunt)
        {
            StartCoroutine(ejecutarTaunt());
        }
    }

    private IEnumerator ejecutarTaunt()
    {
        estaHaciendoTaunt = true;

        // Congela la física con el taunt
        float gravedadOriginal = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = Vector2.zero;

        // Animación de taunt
        animator.SetTrigger("Taunt");

        // Sonido
        if (tauntSound != null)
        {
            audioSource.PlayOneShot(tauntSound);
        }

        // Oculta el sprite original de la rata
        if (spriteRenderer != null)
        {
            spriteRenderer.enabled = false;
        }

        if (tauntImageRenderer != null && tauntSprites != null && tauntSprites.Length > 0)
        {
            int spriteIndex = Mathf.Clamp(activePowerIndex, 0, tauntSprites.Length - 1);
            if (tauntSprites[spriteIndex] != null)
            {
                tauntImageRenderer.sprite = tauntSprites[spriteIndex];
                tauntImageRenderer.gameObject.SetActive(true);
            }
        }

        // Espera la duración del taunt 
        yield return new WaitForSeconds(tauntDuration);

        if (tauntImageRenderer != null)
        {
            tauntImageRenderer.gameObject.SetActive(false);
        }

        // Vuelve a mostrar el sprite original de la rata
        if (spriteRenderer != null)
        {
            spriteRenderer.enabled = true;
        }

        // Las físicas vuelven a la normalidad
        rb.gravityScale = gravedadOriginal;
        estaHaciendoTaunt = false;
    }

    // Función del movimiento horizontal del personaje
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

        // Si está agachado, reduce la velocidad de movimiento
        float velocidadUsada = estaAgachado ? velocidadAgachadoEjeX : velocidadDeMovimientoEjeX;

        rb.velocity = new Vector2(velocidadActualEjeX * velocidadUsada, velocidadActualEjeY);
    }

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

    private void actualizarAnimator()
    {
        animator.SetFloat("velocityX", Mathf.Abs(rb.velocity.x));
        animator.SetFloat("velocityY", rb.velocity.y);
        animator.SetBool("IsGrounded", jugadorEstaTocandoSuelo);
        animator.SetBool("IsCrouching", estaAgachado);
    }
}