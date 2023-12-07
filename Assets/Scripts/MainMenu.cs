using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    public AudioSource effectSound;
    public AudioClip buttonSound;

    private void Start()
    {
        effectSound.clip = buttonSound;
    }

    public void Mapa1()
    {
        effectSound.Play();
        SceneManager.LoadScene("Mapa1");
    }

    public void CargarNivel(string nombreNivel)
    {
        effectSound.Play();
        SceneManager.LoadScene(nombreNivel);
    }

    public void Salir()
    {
        effectSound.Play();
        Application.Quit();
    }

    public void Credits()
    {
        effectSound.Play();
    }
}
