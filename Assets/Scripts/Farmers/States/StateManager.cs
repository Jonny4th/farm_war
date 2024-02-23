using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StateManager
{
    private StateBase currentState;
    public StateBase CurrentState { get { return currentState; } private set { currentState = value; } }

    public void Init(StateBase startState) => CurrentState = startState;
    public void SwitchState(StateBase nextState)
    {
        CurrentState.EndState();
        CurrentState = nextState;
        CurrentState.StartState();
    }
}
