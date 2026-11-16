using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class VolumenScript: MonoBehaviour
{
    //Variable asignada al slider
    public Slider slider;
    //variable asignada al valor del slider
    public float sliderValue;
    //Variable asignada a la imagen del juego
    public Image imagenMute;

    void Start()
    {
        slider.value = PlayerPrefs.GetFloat("volumenAudio", 0.5f);
        AudioListener.volume = slider.value;
        RevisarSiEstoyMute();
    }

    public void ChangeSlider(float valor) {
        sliderValue = valor;
        PlayerPrefs.SetFloat("volumenAudio", sliderValue);
        AudioListener.volume = slider.value;
        RevisarSiEstoyMute();
    }

    public void RevisarSiEstoyMute() {
        if (sliderValue == 0)
        {
            imagenMute.enabled = true;
        }
        else {
            imagenMute.enabled = false;
        }
    }
}
