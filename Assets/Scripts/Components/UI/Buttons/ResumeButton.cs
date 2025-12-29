using System.Collections;
using System.Collections.Generic;
using Scripts.Patterns.State.Components;
using UnityEngine;
using UnityEngine.UI;

public class ResumeButton : MonoBehaviour
{
    private Button btnResume;
    
    void Start()
    {
        btnResume = GetComponent<Button>();
        btnResume.onClick.AddListener(ResumePause); 
    }
    
    void OnDestroy()
    {
        btnResume.onClick.RemoveListener(ResumePause); 
    }

    private void ResumePause()
    {
        GameManager.Instance.ReturnFromPause();
    }
}