using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine//provides simple logic as a state machine to switch between states
{
    public PState CurrentState { get; private set;}//holds current state

    public void Initialize(PState startingState)//initilaizes state
    {
        CurrentState = startingState;
        CurrentState.Enter();
    }

    public void ChangeState(PState newState)//changes current state
    {
        CurrentState.Exit();
        CurrentState = newState;
        CurrentState.Enter();
    }
}
