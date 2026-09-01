using System.Collections;
using System.Collections.Generic;
using GameMain;
using UnityEngine;

public class SquareData : EntityData
{
    public float gravityNum = 0;
    public SquareData(int entityId, int typeId,float gravityNum) : base(entityId, typeId)
    {
        this.gravityNum = gravityNum;
    }
}
