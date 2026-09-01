using System.Collections;
using System.Collections.Generic;
using GameFramework;
using GameFramework.Event;
using UnityEngine;

public class AddScoreEventArgs : GameEventArgs
{
    public static readonly int EventId = typeof(AddScoreEventArgs).GetHashCode();
    
    public override int Id
    {
        get
        {
            return EventId;
        }   
    }

    public int ScoreNum
    {
        get;
        set;
    }
    
    public static AddScoreEventArgs Create(int scoreNum)
    {
        AddScoreEventArgs addScoreEventArgs = ReferencePool.Acquire<AddScoreEventArgs>();
        addScoreEventArgs.ScoreNum = scoreNum;
        return addScoreEventArgs;
    }

    public override void Clear()
    {
        
    }
}
