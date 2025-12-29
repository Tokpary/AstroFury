using System.Collections;
using System.Collections.Generic;
using Scripts.Patterns.State.Components;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuButton : MonoBehaviour
{
    private Button btnMainMenu;
    
    void Start()
    {
        btnMainMenu = GetComponent<Button>();
        btnMainMenu.onClick.AddListener(MainMenu);
    }
    
    void OnDestroy()
    {
        btnMainMenu.onClick.RemoveListener(MainMenu);
    }

    private void MainMenu()
    {
        GameManager.Instance.ReturnMainMenu();
    }
}