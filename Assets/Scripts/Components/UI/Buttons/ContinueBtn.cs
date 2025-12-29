using System.Collections;
using System.Collections.Generic;
using Scripts.Patterns.State.Components;
using Systems.SoundSystem;
using UnityEngine;
using UnityEngine.UI;

public class ContinueBtn : MonoBehaviour
{
    private Button btnContinue;
    
    void Start()
    {
        btnContinue = GetComponent<Button>();
        btnContinue.onClick.AddListener(ContinueGame); 
    }
    
    void OnDestroy()
    {
        btnContinue.onClick.RemoveListener(ContinueGame); 
    }

    private void ContinueGame()
    {
        SoundManager.Instance.PlaySfx("UISelectSFX");
        GameManager.Instance.ContinueGame();
    }
}