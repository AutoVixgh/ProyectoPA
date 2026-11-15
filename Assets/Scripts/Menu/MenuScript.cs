using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuScript : MonoBehaviour
{
    public void jugar() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

     public void salir() {
        Debug.Log("Saliendo del juego...");
        Application.Quit();
    }

    public void volverAlMenuPrincipal() {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MenuPrincipal");
    }
}
