using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine//provides simple logic as a state machine to switch between states
{
    public State CurrentState { get; private set; }//holds current state

    public void Initialize(State startingState)//initilaizes state
    {
        CurrentState = startingState;
        CurrentState.Enter();
    }

    public void ChangeState(State newState)//changes current state
    {
        CurrentState.Exit();
        CurrentState = newState;
        CurrentState.Enter();
    }
}
