using System.Collections;
using System.Collections.Generic;
using GameFramework;
using GameFramework.Event;
using UnityEngine;

public class AddComboEventArgs : GameEventArgs
{
    public static readonly int EventId = typeof(AddComboEventArgs).GetHashCode();
    
    public override int Id
    {
        get
        {
            return EventId;
        }   
    }

    public int ComboNum
    {
        get;
        set;
    }
    
    public static AddComboEventArgs Create(int comboNum)
    {
        AddComboEventArgs addComboEventArgs = ReferencePool.Acquire<AddComboEventArgs>();
        addComboEventArgs.ComboNum = comboNum;
        return addComboEventArgs;
    }

    public override void Clear()
    {
        
    }
}
