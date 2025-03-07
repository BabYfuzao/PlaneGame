using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyManager : MonoBehaviour
{
    public static EnergyManager instance;

    public SmoothBar energyBar;

    public int energy;

    public bool canUlt = false;
    public GameObject ultIcon;

    private void Awake()
    {
        instance = this;
    }

    public void SetDefault(int energyMax)
    {
        energy = 0;
        energyBar.maxValue = energyMax;
        energyBar.SetBar(energy);
    }

    public void ReloadEnergy(WeaponBase weapon, int amount)
    {
        if (!canUlt)
        {
            energy += amount;
            energyBar.SetBar(energy);
            CheckEnergyForUltimate(weapon);
        }
    }

    public void CheckEnergyForUltimate(WeaponBase weapon)
    {
        if (energy >= weapon.energyMax)
        {
            energy = weapon.energyMax;
            canUlt = true;

            ultIcon.GetComponent<SpriteRenderer>().color = Color.red;
        }
    }

    public void ResetEnergy()
    {
        ultIcon.GetComponent<SpriteRenderer>().color = Color.white;
        canUlt = false;
        energy = 0;
        energyBar.SetBar(energy);
    }
}
