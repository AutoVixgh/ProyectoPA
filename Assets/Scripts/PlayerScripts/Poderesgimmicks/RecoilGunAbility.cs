using UnityEngine;

public class RecoilGunAbility : MonoBehaviour
{
    private movimientoDelJugador jugador;
    private Rigidbody2D rb;
    private SpriteRenderer ratSpriteRenderer;

    [Header("Configuración de Disparo y Recoil")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float fuerzaRecoil = 8f;

    [Header("Visuales de la Pistola en la Mano")]
    [SerializeField] private SpriteRenderer gunSpriteRenderer; // El SpriteRenderer de la pistola
    [SerializeField] private Vector3 offsetDerecha = new Vector3(0.3f, 0f, 0f);   // Posición local cuando mira a la derecha
    [SerializeField] private Vector3 offsetIzquierda = new Vector3(-0.3f, 0f, 0f); // Posición local cuando mira a la izquierda

    void Start()
    {
        jugador = GetComponent<movimientoDelJugador>();
        rb = GetComponent<Rigidbody2D>();
        ratSpriteRenderer = GetComponent<SpriteRenderer>();

        if (gunSpriteRenderer != null)
        {
            gunSpriteRenderer.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        // Verificar si el poder de la pistola (índice 5) está seleccionado en la UI
        bool esPoderActivo = (jugador != null && jugador.activePowerIndex == 5);

        if (gunSpriteRenderer != null)
        {
            gunSpriteRenderer.gameObject.SetActive(esPoderActivo);

            if (esPoderActivo)
            {
                bool mirandoIzquierda = ratSpriteRenderer.flipX;

                if (mirandoIzquierda)
                {
                    gunSpriteRenderer.transform.localPosition = offsetIzquierda;
                    gunSpriteRenderer.flipX = true; // Voltea el sprite de la pistola
                }
                else
                {
                    gunSpriteRenderer.transform.localPosition = offsetDerecha;
                    gunSpriteRenderer.flipX = false;
                }
            }
        }

        // Ejecutar disparo solo si el poder está activo y se presiona la tecla M
        if (esPoderActivo && Input.GetKeyDown(KeyCode.M))
        {
            DispararPistolaRecoil();
        }
    }

    void DispararPistolaRecoil()
    {
        bool mirandoIzquierda = ratSpriteRenderer.flipX;
        float dirMovimiento = mirandoIzquierda ? -1f : 1f;

        if (bulletPrefab != null)
        {
            Vector3 spawnPos = firePoint != null ? firePoint.position : transform.position;
            GameObject bullet = Instantiate(bulletPrefab, spawnPos, Quaternion.identity);

            BulletController bulletScript = bullet.GetComponent<BulletController>();
            if (bulletScript != null)
            {
                bulletScript.SetDirection(dirMovimiento);
            }
        }

        // Aplicar el retroceso (recoil) en dirección opuesta
        float direccionRecoil = mirandoIzquierda ? 1f : -1f;
        rb.velocity = new Vector2(direccionRecoil * fuerzaRecoil, rb.velocity.y + 3f);
    }
}