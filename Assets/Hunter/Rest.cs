using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rest : State
{
    public override void Enter(Hunter npc)
    {
        Debug.Log("Entrando en estado de Descanzo");
    }

    public override void Execute(Hunter Cazador)
    {
        Cazador.Energy += Cazador.restRate * Time.deltaTime;

        if (Cazador.Energy >= 100f)
        {
            Cazador.Energy = 100f;
            Cazador.ChangeState(new Patrol());
        }
    }

    public override void Exit(Hunter Cazador)
    {
        Debug.Log("Saliendo del estado de descanzo");
    }
}
