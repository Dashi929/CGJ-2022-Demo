using System.Collections;
using System.Collections.Generic;
using GameFramework;
using GameFramework.Event;
using UnityEngine;

public class GameClearEventArgs : GameEventArgs
{
    public static readonly int EventId = typeof(GameClearEventArgs).GetHashCode();
    
    public override int Id
    {
        get
        {
            return EventId;
        }
    }

    public int CurrentFloorNum
    {
        get;
        set;
    }
    

    public static GameClearEventArgs Create(int currentFloorNum)
    {
        GameClearEventArgs gameClearEventArgs = ReferencePool.Acquire<GameClearEventArgs>();
        gameClearEventArgs.CurrentFloorNum = currentFloorNum;
        return gameClearEventArgs;
    }

    public override void Clear()
    {
        
    }
}
