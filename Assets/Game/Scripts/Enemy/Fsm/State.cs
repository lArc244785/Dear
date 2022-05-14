using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State<T> where T: class {

    public abstract void Enter(T enemy);

    public abstract void Excute(T enemy);

    public abstract void Exit(T enemy);

}
