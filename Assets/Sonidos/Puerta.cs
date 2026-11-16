using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Puerta : MonoBehaviour
{
    [Header("Sprites")]
    [SerializeField] private Sprite spriteAbierta; 

    [Header("Audio")]
    [SerializeField] private AudioClip sonidoAbrir;

    private SpriteRenderer spriteRenderer;
    private AudioSource audioSource;
    private Animator animator;
    private bool estaAbiertaSioNo = false;
    private TilemapRenderer tilemapRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        tilemapRenderer = GetComponent<TilemapRenderer>();

    }


    void Update()
    {
        if (Coleccionable.coleccionablesRecolectados >= 3 && !estaAbiertaSioNo)
        {
            estaAbiertaSioNo = true;
            AbrirPuerta();
        }
    }


    private void AbrirPuerta()
    {
        Debug.Log("puerta abierta");

        if (spriteRenderer != null && spriteAbierta != null)
        {
            spriteRenderer.sprite = spriteAbierta;
        }

        if (audioSource != null && sonidoAbrir != null)
        {
            audioSource.PlayOneShot(sonidoAbrir);
        }

        Collider2D colliderPuerta = GetComponent<Collider2D>();
        if (colliderPuerta != null)
        {
            colliderPuerta.enabled = false;
            tilemapRenderer.enabled = false;

            if (audioSource != null && sonidoAbrir != null)
            {
                audioSource.PlayOneShot(sonidoAbrir);
            }
            
        }
    }
}