using System.Collections;
using System.Collections.Generic;
using GameFramework;
using GameFramework.Event;
using UnityEngine;

public class GameContinueEventArgs : GameEventArgs
{
    public static readonly int EventId = typeof(GameContinueEventArgs).GetHashCode();
    
    public override int Id
    {
        get
        {
            return EventId;
        }
    }
    

    public static GameContinueEventArgs Create()
    {
        GameContinueEventArgs gameContinueEventArgs = ReferencePool.Acquire<GameContinueEventArgs>();
        return gameContinueEventArgs;
    }

    public override void Clear()
    {
        
    }
}
