using System.Collections;
using System.Collections.Generic;
//remove unneeded librarys?
using UnityEngine;

public class State // no monobehaviour as this is used more as a template and not meant to be put on a game object
{
    protected KuroCore Core;//passes in script that is on the player
    protected StateMachine stateMachine;//passes in statemachine
    //these 2 variables initiliaze what a state needs in order to be constructed

    protected bool isAnimationFinished;//this bool is used in states as a signal when an animation is finished, this helps with frame by frame accuracy

    protected bool Animationtriggered;//see above

    protected float startTime;//used to keep track of how long one is in a state 

    private string animBoolName;//used to communicate to the animator

    public State(KuroCore core, StateMachine stateMachine, string animBoolName)//constructor, all states need these
    {
        this.Core = core;
        this.stateMachine = stateMachine;
        this.animBoolName = animBoolName;

    }

    public virtual void Enter()//what happens when entering a state
    {
        DoCheck();
        Core.Anim.SetBool(animBoolName, true);//set animator upon entering a state
        startTime = Time.time;
        //Debug.Log(animBoolName);
        Animationtriggered = false;
        isAnimationFinished = false;//sets animation triggers to false when entering state
    }

    public virtual void Exit()//what happens when you leave a certain state
    {
        Core.Anim.SetBool(animBoolName, false);//sets animator to false so next state can be activated
    }

    public virtual void LogicUpdate()//called every frame like update
    {

    }

    public virtual void PhysicsUpdate()//called like fixed update
    {
        DoCheck();
    }

    public virtual void DoCheck() // used to look for ground walls etc once instead of fixed update
    {

    }

    public virtual void AnimationTrigger() => Animationtriggered = true;

    public virtual void AnimationFinishTrigger() => isAnimationFinished = true;
}
