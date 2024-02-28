using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StateManager
{
    [SerializeField] private StateBase currentState;
    public StateBase CurrentState { get { return currentState; } private set { currentState = value; } }

    public void Init(StateBase startState)
    {
        Debug.Log("FFFFFFFFFFFF");
        CurrentState = startState;
        CurrentState.StartState();
    }
    public void SwitchState(StateBase nextState)
    {
        CurrentState.EndState();
        CurrentState = nextState;
        CurrentState.StartState();
    }
}
