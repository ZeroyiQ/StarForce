//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using GameFramework;
using GameFramework.DataTable;
using UnityEngine;
using UnityGameFramework.Runtime;

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

        public override void Initialize(BinBall ball)
        {
            base.Initialize(ball);
            if (ball != null)
            {
                ball.PauseBall();
            }else{
                Log.Error("实例空");
            }
        }

        public override void Shutdown()
        {
            base.Shutdown();
        }

        public override void Update(float elapseSeconds, float realElapseSeconds)
        {
            base.Update(elapseSeconds, realElapseSeconds);

        }
    }
}
