using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State 
{

    public abstract void Enter(Hunter cazador);
    public abstract void Execute(Hunter cazador);
    public abstract void Exit(Hunter cazador);
}
