    using System.Collections;
using System.Collections.Generic;
using GameFramework;
using GameFramework.Event;
using UnityEngine;

public class TimeOutEventArgs : GameEventArgs
{
    public static readonly int EventId = typeof(TimeOutEventArgs).GetHashCode();
    
    public override int Id
    {
        get
        {
            return EventId;
        }   
    }

    public static TimeOutEventArgs Create()
    {
        TimeOutEventArgs timeOutEventArgs = ReferencePool.Acquire<TimeOutEventArgs>();
        return timeOutEventArgs;
    }

    public override void Clear()
    {
        
    }
}
