using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CondicionVictoria : MonoBehaviour
{
    public movimientoDelJugador playerMovement;
    public VictoryPanel victoryPanel;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Victoria")) {
            playerMovement.enabled = false;
            victoryPanel.activarVictoria();
        }
    }
}
