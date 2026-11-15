using UnityEngine;

public class Coleccionable : MonoBehaviour
{
    public static int coleccionablesRecolectados = 0;

    [Header("Audio")]
    [SerializeField] private AudioClip sonidoRecoleccion;

    private void ColeccionableColisionaConJugador(Collider2D collision)
    {
        
        if (collision.CompareTag("Player"))
        {
            coleccionablesRecolectados++;
            Debug.Log("coleccionables recolectados: " + coleccionablesRecolectados);
            
            if (sonidoRecoleccion != null)
            {
                AudioSource.PlayClipAtPoint(sonidoRecoleccion, transform.position);
            }

            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        ColeccionableColisionaConJugador(collision);
    }
}