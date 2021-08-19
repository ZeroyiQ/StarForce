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
    public class BuildGame : GameBase
    {
        public override GameMode GameMode
        {
            get
            {
                return GameMode.Build;
            }
        }

        public override void Initialize()
        {
            base.Initialize();
            GameEntry.Base.PauseGame();
        }

        public override void Shutdown()
        {
            base.Shutdown();
            GameEntry.Base.ResumeGame();
        }

        public override void Update(float elapseSeconds, float realElapseSeconds)
        {
            base.Update(elapseSeconds, realElapseSeconds);

        }
    }
}
