using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class VictoryPanel : MonoBehaviour
{
    public GameObject victoryPanel;
    public void activarVictoria()
    {
        if (victoryPanel != null)
        {
            victoryPanel.SetActive(true);
            Time.timeScale = 0f; // pausa el juego
        }
    }

    // esta funcion es para cuando se clickee el boton de "reiniciar" y la escena se reinicie.
    public void ReiniciarJuego()
    {
        Time.timeScale = 1f; // reanuda el tiempo antes de recargar

        // recarga la escena
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void PasarNivel() {
        Time.timeScale = 1f;
        // Pasa al siguiente nivel dependiendo del indice
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
