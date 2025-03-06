using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyManager : MonoBehaviour
{
    public static EnergyManager instance;

    public SmoothBar energyBar;

    public int energy;
    public bool canUlt = false;

    private void Awake()
    {
        instance = this;
    }

    public void SetDefault(int energyMax)
    {
        energyBar.maxValue = energyMax;
        energyBar.UpdateBar();
    }

    public virtual void ReloadEnergy(WeaponBase weapon, int amount)
    {
        if (!canUlt)
        {
            energy += amount;
            energyBar.SetBar(amount);
            CheckEnergyForUltimate(weapon);
        }
    }

    public virtual void CheckEnergyForUltimate(WeaponBase weapon)
    {
        if (energy >= weapon.energyMax)
        {
            energy = weapon.energyMax;
            canUlt = true;
        }
    }
}
