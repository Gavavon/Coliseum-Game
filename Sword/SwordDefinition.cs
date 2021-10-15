using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordDefinition : MonoBehaviour
{
    public enum ElementalState
    {
        Hot,
        Cold
    }
    public ElementalState currentElementState { get; set; }

    public enum WeaponState
    {
        Sheathed,
        UnSheathed
    }
    public WeaponState currentWeaponState { get; set; }

    public static SwordDefinition instance;
    private void Awake()
    {
        instance = this;
    }
}
