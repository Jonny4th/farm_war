using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    private StateBase currentState;
    public StateBase CurrentState { get { return currentState; } set { currentState = value; } }

    public void SwitchState(StateBase nextState)
    {
        CurrentState.EndState();
        CurrentState = nextState;
    }
}
