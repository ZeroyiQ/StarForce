using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace StarForce
{
    [Serializable]
    public class BinballData : EntityData
    {
        public BinballData(int entityId, int typeId) : base(entityId, typeId)
        {
        }
    }
}