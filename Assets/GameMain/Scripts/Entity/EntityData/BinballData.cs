using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace StarForce
{
    [Serializable]
    public class BinballData : EntityData
    {
        [SerializeField]
        private string m_Name = null;
        [SerializeField]
        private Vector3 m_Position;
        public BinballData(int entityId, int typeId) : base(entityId, typeId)
        {
        }
         /// <summary>
        /// 角色名称。
        /// </summary>
        public string Name
        {
            get
            {
                return m_Name;
            }
            set
            {
                m_Name = value;
            }
        }

    }
}