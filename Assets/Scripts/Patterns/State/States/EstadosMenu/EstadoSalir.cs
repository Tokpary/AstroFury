using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EstadoSalir : IStateManager
{
    public void EnterState(StateManager manager)
    {
        manager.creditsMenu.SetActive(false);
        manager.mainMenu.SetActive(false);

        // Cerrar la aplicación
        Application.Quit();
    }

    public void UpdateState(StateManager manager) { }

    public void OnPlayButton(StateManager manager) { }

    public void OnCreditsButton(StateManager manager) { }

    public void OnExitButton(StateManager manager) { }

    public void OnBackButton(StateManager manager) { }
}