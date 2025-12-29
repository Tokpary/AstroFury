using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStateManager{
    public void EnterState(StateManager manager);
    public void UpdateState(StateManager manager);
    public void OnPlayButton(StateManager manager);
    public void OnCreditsButton(StateManager manager);
    public void OnExitButton(StateManager manager);
    public void OnBackButton(StateManager manager);
}
