using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

public class DebugCommandBase 
{
    string _commandID;
    string _commandDescription;
    string _commandFormat;

    public string commandID {get { return _commandID; } }
    public string commandDescription { get { return _commandDescription; } }
    public string commandFormat {  get { return _commandFormat; } }
    public DebugCommandBase(string id,string description,string format)
    {
        _commandID = id;
        _commandDescription = description;
        _commandFormat = format;
    }
}
public class DebugCommand : DebugCommandBase
{
    private Action command;
    public DebugCommand(string id, string description, string format, Action command) : base(id, description, format)
    {
        this.command = command;
    }
    public void Invoke()
    {
        command.Invoke();
    }
}
public class DebugCommand<T>: DebugCommandBase
{
    private Action<T> command;
    public DebugCommand(string id, string description, string format, Action<T> command) : base(id, description, format)
    {
        this.command = command;
    }
    public void Invoke(T value)
    {
        command.Invoke(value);
    }
}
public class DebugCommand<T1,T2> : DebugCommandBase
{
    private Action<T1, T2> command;
    public DebugCommand(string id, string description, string format, Action<T1, T2> command) : base(id, description, format)
    {
        this.command = command;
    }
    public void Invoke(T1 t1, T2 t2)
    {
        command.Invoke(t1,t2);
    }
}
public class DebugCommand<T1,T2,T3> : DebugCommandBase
{
    private Action<T1, T2, T3> command;
    public DebugCommand(string id, string description, string format, Action<T1, T2, T3> command) : base(id, description, format)
    {
        this.command = command;
    }
    public void Invoke(T1 t1, T2 t2, T3 t3)
    {
        command.Invoke(t1,t2,t3);
    }
}
