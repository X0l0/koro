using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PState // no monobehaviour as this is used more as a template and not meant to be put on a game object
{
    protected Player player;//passes in script that is on the player
    protected PlayerStateMachine stateMachine;//passes in statemachine
    //protected PlayerData playerData;//passes in player data
    //these 3 variables initiliaze what a state needs in order to be constructed

    protected bool isAnimationFinished;//this bool is used in states as a signal when an animation is finished, this helps with frame by frame accuracy

    protected bool Animationtriggered;//see above

    protected float startTime;//used to keep track of how long one is in a state 

    private string animBoolName;//used to communicate to the animator

    public PState(Player player, PlayerStateMachine stateMachine, string animBoolName)//constructor
    {
        this.player = player;
        this.stateMachine = stateMachine;
        //this.playerData = playerData;
        this.animBoolName = animBoolName;

    }

    public virtual void Enter()//what happens when entering a state
    {
        DoCheck();
        player.Anim.SetBool(animBoolName, true);//set animator upon entering a state
        startTime = Time.time;
        Debug.Log(animBoolName);
        Animationtriggered = false;
        isAnimationFinished = false;//sets animation to not finished when entering state
    }

    public virtual void Exit()//what happens when you leave a certain state
    {
        player.Anim.SetBool(animBoolName, false);
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

    public virtual void AnimationFinishTrigger()  => isAnimationFinished = true;
}
