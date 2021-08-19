//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using GameFramework;
using GameFramework.DataTable;
using UnityEngine;

namespace BinBall
{
    public class ShowGame : GameBase
    {
        public override GameMode GameMode
        {
            get
            {
                return GameMode.Show;
            }
        }
        public float Sorce
        {
            get;
            private set;
        }

        public override void Initialize()
        {
            base.Initialize();
            Sorce = 0;
        }

        public override void Shutdown()
        {
            base.Shutdown();
        }

        public override void Update(float elapseSeconds, float realElapseSeconds)
        {
            //base.Update(elapseSeconds, realElapseSeconds);
            Sorce += realElapseSeconds * 0.5f;
        }
    }
}
