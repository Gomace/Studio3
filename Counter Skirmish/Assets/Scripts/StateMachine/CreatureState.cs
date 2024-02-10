using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class CreatureState
{
    virtual public CreatureState handleInput()
    {
        return this;
    }
}

class IdleState : CreatureState
{
    override public CreatureState handleInput()
    {
        return this;
    }
}

class JumpingState : CreatureState
{
    override public CreatureState handleInput()
    {
        return this;
    }
}
