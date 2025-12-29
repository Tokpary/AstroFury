using System.Collections;
using System.Collections.Generic;
using Systems.SoundSystem;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class StateManager : MonoBehaviour
{
    //Variables
    public GameObject mainMenu;
    public GameObject creditsMenu;
    public IStateManager currentState;

    public IStateManager GetState() { return currentState; }

    public void SetState(IStateManager newState)
    {
        //Cambiar el estado y entrar
        currentState = newState;
        currentState.EnterState(this);
    }

    void Start()
    {
        SetState(new EstadoMenuPrincipal());
    }


    
    void Update()
    {
        if (currentState != null)
        {
            currentState.UpdateState(this);
        }
    }

    public void PlayNowButton()
    {
        SoundManager.Instance.PlaySfx("UISelectSFX");
        currentState.OnPlayButton(this);
    }

    public void CreditsButton()
    {
        SoundManager.Instance.PlaySfx("UISelectSFX");
        currentState.OnCreditsButton(this);
    }

    public void QuitButton()
    {
        SoundManager.Instance.PlaySfx("UISelectSFX");
        currentState.OnExitButton(this);
    }

    public void BackButton()
    {
        SoundManager.Instance.PlaySfx("UISelectSFX");
        currentState.OnBackButton(this);
    }
}
   