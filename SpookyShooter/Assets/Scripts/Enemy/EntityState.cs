using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntityState
{
    public abstract void OnStateEnter();
    public abstract void OnStateUpdate();
    public abstract void OnStateExit();
    public virtual void OnStateFixedUpdate() { }


}
