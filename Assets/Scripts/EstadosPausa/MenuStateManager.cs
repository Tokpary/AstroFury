using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuStateManager : MonoBehaviour
{
    public GameObject pauseMenu;
    private IMenuState currentState;

    public IMenuState GetState() { return currentState; }

    public void SetState(IMenuState newState)
    {
        if (currentState != null)
        {
            currentState.ExitState(this);
        }

        currentState = newState;
        currentState.EnterState(this);
    }

    void Start()
    {

        SetState(new EstadoJuego()); 
    }

    void Update()
    {
        if (currentState != null)
        {
            currentState.UpdateState(this);
        }
    }

    public void PauseButtonPressed()
    {
        currentState.OnPauseButton(this);
    }

    public void ResumeButtonPressed()
    {
        currentState.OnResumeButton(this);
    }

    public void MainMenuButtonPressed()
    {
        currentState.OnMainMenuButton(this);
    }

    public void QuitButtonPressed()
    {
        currentState.OnQuitButton(this);
    }
}
