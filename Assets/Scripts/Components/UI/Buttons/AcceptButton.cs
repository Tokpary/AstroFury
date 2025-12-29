using System.Collections;
using System.Collections.Generic;
using Scripts.Patterns.State.Components;
using UnityEngine;
using UnityEngine.UI;

public class AcceptButton : MonoBehaviour
{
    private Button btnAccept;
    
    void Start()
    {
        btnAccept = GetComponent<Button>();
        btnAccept.onClick.AddListener(AcceptUpgrade);
    }
    
    void OnDestroy()
    {
        btnAccept.onClick.RemoveListener(AcceptUpgrade);
    }

    private void AcceptUpgrade()
    {
        GameManager.Instance.AcceptUpgrade();
    }
}