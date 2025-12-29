using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EstadoJuego : IMenuState
{
    public void EnterState(MenuStateManager manager)
    {
        manager.pauseMenu.SetActive(false); // Asegúrate de que el menú de pausa esté desactivado
    }

    public void ExitState(MenuStateManager manager) { }

    public void UpdateState(MenuStateManager manager)
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            manager.SetState(new EstadoPausa());
        }
    }

    public void OnPauseButton(MenuStateManager manager)
    {
        manager.SetState(new EstadoPausa());
    }

    public void OnResumeButton(MenuStateManager manager) { }

    public void OnMainMenuButton(MenuStateManager manager) { }

    public void OnQuitButton(MenuStateManager manager) { }
}
