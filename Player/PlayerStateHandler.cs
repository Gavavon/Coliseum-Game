using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateHandler : MonoBehaviour
{
    public enum State
    {
        NormIdle,
        NormWalk,
        NormRun,
        CombIdle,
        CombWalk,
        CombBlock,
        CombAttack
    }
    public State m_State;
    public State m_CurrentState { get; set; }

    void Start()
    {
        m_CurrentState = m_State;
    }
}
