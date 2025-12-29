using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EstadoPausa : IMenuState
{
    public void EnterState(MenuStateManager manager)
    {
        manager.pauseMenu.SetActive(true); 
        Time.timeScale = 0f; // Pausar el juego
    }

    public void ExitState(MenuStateManager manager)
    {
        manager.pauseMenu.SetActive(false); // Desactivar el menú de pausa
        Time.timeScale = 1f; // Reanudar el tiempo cuando se sale del estado de pausa
    }

    public void UpdateState(MenuStateManager manager) {
    }

    public void OnPauseButton(MenuStateManager manager) { }

    public void OnResumeButton(MenuStateManager manager)
    {
        manager.SetState(new EstadoJuego());
    }

    public void OnMainMenuButton(MenuStateManager manager)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }

    public void OnQuitButton(MenuStateManager manager)
    {
        manager.pauseMenu.SetActive(false);
        Application.Quit();
    }
}
