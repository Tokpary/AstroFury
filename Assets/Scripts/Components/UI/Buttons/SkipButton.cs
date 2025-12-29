using System.Collections;
using System.Collections.Generic;
using Scripts.Patterns.State.Components;
using UnityEngine;
using UnityEngine.UI;

public class SkipButton : MonoBehaviour
{
    private Button btnSkip;
    
    void Start()
    {
        btnSkip = GetComponent<Button>();
        btnSkip.onClick.AddListener(SkipUpgrade);
    }
    
    void OnDestroy()
    {
        btnSkip.onClick.RemoveListener(SkipUpgrade);
    }

    private void SkipUpgrade()
    {
        GameManager.Instance.SkipUpgrade();
    }
}