using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EstadoCreditos : IStateManager
{
    public void EnterState(StateManager manager)
    {
        Debug.Log("Entered Credits State");
        manager.mainMenu.SetActive(false);
        manager.creditsMenu.SetActive(true);

    }

    public void UpdateState(StateManager manager)
    {
        // Lógica de actualización del estado de créditos

    }

    public void OnPlayButton(StateManager manager) { }

    public void OnCreditsButton(StateManager manager) { }

    public void OnExitButton(StateManager manager) { }
    public void OnBackButton(StateManager manager)
    {
        manager.SetState(new EstadoMenuPrincipal());
    }

}
