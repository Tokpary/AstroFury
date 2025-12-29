using System.Collections;
using System.Collections.Generic;
using Scripts.Patterns.State.Components;
using Systems.SoundSystem;
using UnityEditor;
using UnityEngine;

public class EstadoMenuPrincipal : IStateManager
{
   
    public void EnterState(StateManager manager)
    {
        SoundManager.Instance.PlayMusic("MenuMusic");
        manager.mainMenu.SetActive(true);
        manager.creditsMenu.SetActive(false);
    }

    public void UpdateState(StateManager manager)
    {
        
    }

    public void OnPlayButton(StateManager manager)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameLevel");
    }

    public void OnCreditsButton(StateManager manager)
    {
        manager.SetState(new EstadoCreditos());
        
    }

    public void OnExitButton(StateManager manager)
    {
        manager.SetState(new EstadoSalir());
    }

    public void OnBackButton(StateManager manager) {}
}


