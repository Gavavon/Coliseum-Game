using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatController : MonoBehaviour
{
    public bool inCombat = false;

    public static CombatController instance;
    private void Awake()
    {
        instance = this;
    }
}
