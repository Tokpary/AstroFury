using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace.Patterns;
using Patterns.State.Interfaces;
using Scripts.Patterns.State.Components;
using Scripts.Patterns.State.States;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputActionManager : MonoBehaviour
{
    [SerializeField] private InputActionAsset inputActionAsset;
    
    public delegate void OnFireAction();
    public static event OnFireAction OnFiredStarted;
    public static event OnFireAction OnFiredEnded;
    
    public delegate void OnAimAction(InputAction.CallbackContext context);
    public static event OnAimAction OnAimed;
    
    public void OnPause (InputAction.CallbackContext context)
    {
        if(context.started)
            GameManager.Instance.PauseGame();
    }
    
    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            OnFiredStarted?.Invoke();
        }
        
        if (context.canceled)
        {
            OnFiredEnded?.Invoke();
        }
    }
    
    
    public void OnAim(InputAction.CallbackContext context)
    {
        
        OnAimed?.Invoke(context);
        
    }
    
    private void Start()
    {
        inputActionAsset.Enable();
        inputActionAsset.FindActionMap("Player").Disable();
        inputActionAsset.FindActionMap("UI").Enable();
        GameManager.Instance.OnStateChanged += OnStateChanged;
    }

    private void OnStateChanged(IState state)
    {
        if (state is PlayingState)
        {
            inputActionAsset.FindActionMap("Player").Enable();
            inputActionAsset.FindActionMap("UI").Disable();
        }
        else
        {
            inputActionAsset.FindActionMap("Player").Disable();
            inputActionAsset.FindActionMap("UI").Enable();
        }
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnStateChanged -= OnStateChanged;
        inputActionAsset.Disable();
    }
}
