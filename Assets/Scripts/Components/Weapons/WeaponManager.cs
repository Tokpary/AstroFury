using System;
using System.Collections;
using System.Collections.Generic;
using Components;
using Components.Weapons;
using Scripts.Patterns.State.Components;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    private List<AWeapon> weapons;
    private AWeapon currentWeapon;

    private void Start()
    {
        weapons = new List<AWeapon>(GetComponentsInChildren<AWeapon>(true));
        InputActionManager.OnFiredStarted += StartShooting;
        InputActionManager.OnFiredEnded += EndShooting;
        GameManager.Instance.OnWeaponAcquired.AddListener(SwitchWeapon);
        GameManager.Instance.OnGameRestarted.AddListener(SetInitialWeapon);
        currentWeapon = weapons.Find(weapon => weapon.isActiveAndEnabled);
        SwitchWeapon("ULTRA RAPID FIRECANNON");
    }

    private void EndShooting()
    {
        currentWeapon.EndShooting();
    }

    private void StartShooting()
    {
        currentWeapon.StartShooting();
    }

    private void SetInitialWeapon()
    {
        currentWeapon.gameObject.SetActive(false);
        SwitchWeapon("ULTRA RAPID FIRECANNON");
        currentWeapon.gameObject.SetActive(true);
        
    }

    private void OnDestroy()
    {
        InputActionManager.OnFiredStarted -= StartShooting;
        InputActionManager.OnFiredEnded -= EndShooting;
        GameManager.Instance.OnWeaponAcquired.RemoveListener(SwitchWeapon);
        GameManager.Instance.OnGameRestarted.RemoveListener(SetInitialWeapon);
    }

    public void SwitchWeapon(string name)
    {
        AWeapon weapon = weapons.Find(w => w.weapon.ItemName == name);
        if (weapon == null)
        {
            throw new ArgumentException("Weapon not found", "name");
        }

        currentWeapon.gameObject.SetActive(false);
        currentWeapon = weapon;
        currentWeapon.gameObject.SetActive(true);
    }
    
    
}
