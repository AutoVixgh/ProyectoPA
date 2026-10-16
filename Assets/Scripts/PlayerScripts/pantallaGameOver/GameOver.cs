using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public GameObject GameOverPanel;

    // el panel del game over por defecto esta desactivado, cuando muera la rata, se llama a esta funcion
    public void ActivarGameOver()
    {
        if (GameOverPanel != null)
        {
            GameOverPanel.SetActive(true);
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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
