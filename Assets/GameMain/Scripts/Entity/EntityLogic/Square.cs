using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;

public class Square : EntityLogic
{
    private SquareData m_SquareData = null;
    protected override void OnShow(object userData)
    {
        base.OnShow(userData);
        m_SquareData = userData as SquareData;
        if (m_SquareData == null)
        {
            Log.Error("Entity data is invalid.");
            return;
        }
        
    }
}
