using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Victoria : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private AudioClip sonidoDeLaVictoria;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
        
            if (panel != null)
            {
                Debug.Log("Victoria");
                panel.SetActive(true);
                Time.timeScale = 0f;   

                if (sonidoDeLaVictoria != null)
                {
                    AudioSource.PlayClipAtPoint(sonidoDeLaVictoria, transform.position, 1.5f);
                }
            }
            else
            {
                Debug.LogError("panel no encontrado / asignado");
            }
        }
    }
}