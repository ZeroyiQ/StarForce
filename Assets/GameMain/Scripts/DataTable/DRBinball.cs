﻿//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------
// 此文件由工具自动生成，请勿直接修改。
// 生成时间：2021-08-18 16:49:40.308
//------------------------------------------------------------

using GameFramework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace StarForce
{
    /// <summary>
    /// 球类表。
    /// </summary>
    public class DRBinball : DataRowBase
    {
        private int m_Id = 0;

        /// <summary>
        /// 获取球体编号。
        /// </summary>
        public override int Id
        {
            get
            {
                return m_Id;
            }
        }

        /// <summary>
        /// 获取质量。
        /// </summary>
        public float Mass
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取阻力。
        /// </summary>
        public float Drag
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取摩擦力。
        /// </summary>
        public float Friction
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取弹力。
        /// </summary>
        public float Bounciness
        {
            get;
            private set;
        }

        public override bool ParseDataRow(string dataRowString, object userData)
        {
            string[] columnStrings = dataRowString.Split(DataTableExtension.DataSplitSeparators);
            for (int i = 0; i < columnStrings.Length; i++)
            {
                columnStrings[i] = columnStrings[i].Trim(DataTableExtension.DataTrimSeparators);
            }

            int index = 0;
            index++;
            m_Id = int.Parse(columnStrings[index++]);
            index++;
            Mass = float.Parse(columnStrings[index++]);
            Drag = float.Parse(columnStrings[index++]);
            Friction = float.Parse(columnStrings[index++]);
            Bounciness = float.Parse(columnStrings[index++]);

            GeneratePropertyArray();
            return true;
        }

        public override bool ParseDataRow(byte[] dataRowBytes, int startIndex, int length, object userData)
        {
            using (MemoryStream memoryStream = new MemoryStream(dataRowBytes, startIndex, length, false))
            {
                using (BinaryReader binaryReader = new BinaryReader(memoryStream, Encoding.UTF8))
                {
                    m_Id = binaryReader.Read7BitEncodedInt32();
                    Mass = binaryReader.ReadSingle();
                    Drag = binaryReader.ReadSingle();
                    Friction = binaryReader.ReadSingle();
                    Bounciness = binaryReader.ReadSingle();
                }
            }

            GeneratePropertyArray();
            return true;
        }

        private void GeneratePropertyArray()
        {

        }
    }
}
