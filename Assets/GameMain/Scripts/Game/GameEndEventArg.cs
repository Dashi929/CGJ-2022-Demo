using System.Collections;
using System.Collections.Generic;
using GameFramework;
using GameFramework.Event;
using UnityEngine;

public class GameFailEventArgs : GameEventArgs
{
    public static readonly int EventId = typeof(GameFailEventArgs).GetHashCode();
    
    public override int Id
    {
        get
        {
            return EventId;
        }
    }


    

    public static GameFailEventArgs Create()
    {
        GameFailEventArgs gameEndEventArgs = ReferencePool.Acquire<GameFailEventArgs>();
        return gameEndEventArgs;
    }

    public override void Clear()
    {
        
    }
}
