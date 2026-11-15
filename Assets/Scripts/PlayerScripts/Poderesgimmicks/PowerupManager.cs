using UnityEngine;
using UnityEngine.UI;

public class PowerUpManager : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private movimientoDelJugador jugadorMovimiento; // Referencia al script de ratatorta

    [Header("UI Slots (Esquina Superior Izquierda)")]
    [SerializeField] private Image uiImgIzquierda; // El poder anterior (más pequeño)
    [SerializeField] private Image uiImgCentro;    // El poder activo actual (grande)
    [SerializeField] private Image uiImgDerecha;   // El poder siguiente (más pequeño)

    [Header("Configuración de Gimmicks")]
    // [0] = nada
    // [1] = Slam, [2] Air Dash, [3] Hookshot, [4] SuperJump, [5] RecoilGun
    [SerializeField] private Sprite[] powerUpSprites;

    private int currentIndex = 0;
    private int totalPowers = 6;

    void Start()
    {
        if (jugadorMovimiento == null)
        {
            jugadorMovimiento = FindObjectOfType<movimientoDelJugador>();
        }

        ActualizarUIYPoder();
    }

    void Update()
    {
        ManejarCambioDePoderes();
    }

    private void ManejarCambioDePoderes()
    {
        // Tecla Q: Mueve hacia la izquierda (poder anterior)
        if (Input.GetKeyDown(KeyCode.Q))
        {
            currentIndex--;
            if (currentIndex < 0)
            {
                currentIndex = totalPowers - 1;
            }
            ActualizarUIYPoder();
        }

        // Tecla E: Mueve hacia la derecha (poder siguiente)
        if (Input.GetKeyDown(KeyCode.E))
        {
            currentIndex++;
            if (currentIndex >= totalPowers)
            {
                currentIndex = 0;
            }
            ActualizarUIYPoder();
        }
    }

    private void ActualizarUIYPoder()
    {
        if (jugadorMovimiento != null)
        {
            jugadorMovimiento.activePowerIndex = currentIndex;
        }

        int indexIzquierda = (currentIndex - 1 + totalPowers) % totalPowers;
        int indexDerecha = (currentIndex + 1) % totalPowers;

        AsignarSpriteUI(uiImgCentro, powerUpSprites[currentIndex]);

        AsignarSpriteUI(uiImgIzquierda, powerUpSprites[indexIzquierda]);

        AsignarSpriteUI(uiImgDerecha, powerUpSprites[indexDerecha]);
    }

    private void AsignarSpriteUI(Image uiImage, Sprite spriteNuevo)
    {
        if (uiImage != null)
        {
            if (spriteNuevo != null)
            {
                uiImage.gameObject.SetActive(true);
                uiImage.sprite = spriteNuevo;
            }
            else
            {
                uiImage.gameObject.SetActive(false);
            }
        }
    }
}