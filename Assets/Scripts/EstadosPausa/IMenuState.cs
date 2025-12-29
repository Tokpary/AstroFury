using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMenuState
{
    void EnterState(MenuStateManager manager);
    void ExitState(MenuStateManager manager);
    void UpdateState(MenuStateManager manager);
    void OnPauseButton(MenuStateManager manager);
    void OnResumeButton(MenuStateManager manager);
    void OnMainMenuButton(MenuStateManager manager);
    void OnQuitButton(MenuStateManager manager);
}
