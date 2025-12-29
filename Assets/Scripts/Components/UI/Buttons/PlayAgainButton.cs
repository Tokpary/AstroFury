using System.Collections;
using System.Collections.Generic;
using Scripts.Patterns.State.Components;
using Systems.SoundSystem;
using UnityEngine;
using UnityEngine.UI;

public class PlayAgainButton : MonoBehaviour
{
    private Button btnPlayAgain;
    
    void Start()
    {
        btnPlayAgain = GetComponent<Button>();
        btnPlayAgain.onClick.AddListener(StartGame); 
    }
    
    void OnDestroy()
    {
        btnPlayAgain.onClick.RemoveListener(StartGame); 
    }

    private void StartGame()
    {
        SoundManager.Instance.PlaySfx("UISelectSFX");
        GameManager.Instance.StartGame();
    }
}
