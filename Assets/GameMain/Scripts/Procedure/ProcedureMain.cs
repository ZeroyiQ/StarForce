//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using System.Collections.Generic;
using UnityGameFramework.Runtime;
using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;
using GameFramework.Event;
using UnityEngine;

namespace BinBall
{
    public class ProcedureMain : ProcedureBase
    {
        public override bool UseNativeDialog
        {
            get
            {
                return false;
            }
        }
        private const float GameOverDelayedSeconds = 2f;

        private readonly Dictionary<GameMode, GameBase> m_Games = new Dictionary<GameMode, GameBase>();
        private GameBase m_CurrentGame = null;
        private bool m_GotoMenu = false;
        private float m_GotoMenuDelaySeconds = 0f;

        private MainForm m_MainForm;
        private int m_Id;
        private BinBall m_MyBall;
        private bool m_Ready;
        public void GotoMenu()
        {
            m_GotoMenu = true;
        }

        #region Life Cycle

        protected override void OnInit(ProcedureOwner procedureOwner)
        {
            base.OnInit(procedureOwner);

            // m_Games.Add(GameMode.Survival, new SurvivalGame());
            m_Games.Add(GameMode.Build, new BuildGame());
            m_Games.Add(GameMode.Show, new ShowGame());
        }

        protected override void OnDestroy(ProcedureOwner procedureOwner)
        {
            base.OnDestroy(procedureOwner);
            m_Games.Clear();
        }

        protected override void OnEnter(ProcedureOwner procedureOwner)
        {
            base.OnEnter(procedureOwner);
            GameEntry.Event.Subscribe(OpenUIFormSuccessEventArgs.EventId, OnOpenMainUI);
            GameEntry.Event.Subscribe(ShowEntitySuccessEventArgs.EventId, OnShowEntitySuccess);
            m_GotoMenu = false;
            m_Ready = false;
            InitBinBall();
        }

        protected override void OnLeave(ProcedureOwner procedureOwner, bool isShutdown)
        {
            GameEntry.Event.Unsubscribe(OpenUIFormSuccessEventArgs.EventId, OnOpenMainUI);
            GameEntry.Event.Unsubscribe(ShowEntitySuccessEventArgs.EventId, OnShowEntitySuccess);

            if (m_CurrentGame != null)
            {
                m_CurrentGame.Shutdown();
                m_CurrentGame = null;
            }
            if (m_MainForm != null)
            {
                m_MainForm.Close(isShutdown);
                m_MainForm = null;
            }
            m_MyBall = null;
            base.OnLeave(procedureOwner, isShutdown);
        }

        protected override void OnUpdate(ProcedureOwner procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);
            if (!m_Ready)
            {
                return;
            }
            if (m_CurrentGame != null && !m_CurrentGame.GameOver)
            {
                m_CurrentGame.Update(elapseSeconds, realElapseSeconds);
                if (m_CurrentGame.GameMode == GameMode.Show)
                {
                    m_MainForm.SetScoreText(((ShowGame)m_CurrentGame).Score);
                }
                return;
            }

            if (!m_GotoMenu)
            {
                m_GotoMenu = true;
                m_GotoMenuDelaySeconds = 0;
            }

            m_GotoMenuDelaySeconds += elapseSeconds;
            if (m_GotoMenuDelaySeconds >= GameOverDelayedSeconds)
            {
                procedureOwner.SetData<VarInt32>("NextSceneId", GameEntry.Config.GetInt("Scene.Menu"));
                ChangeState<ProcedureChangeScene>(procedureOwner);
            }
        }

        #endregion


        #region Event

        private void OnOpenMainUI(object sender, GameEventArgs e)
        {
            OpenUIFormSuccessEventArgs ne = (OpenUIFormSuccessEventArgs)e;
            if (ne.UserData != this)
            {
                return;
            }

            m_MainForm = (MainForm)ne.UIForm.Logic;
            SetGameMode(GameMode.Build);
            m_Ready = true;
        }
        private void OnShowEntitySuccess(object sender, GameEventArgs e)
        {
            ShowEntitySuccessEventArgs ne = (ShowEntitySuccessEventArgs)e;
            if (m_MyBall != null || ne.Entity.Id != m_Id)
            {
                return;
            }
            m_MyBall = (BinBall)ne.Entity.Logic;
            GameEntry.UI.OpenUIForm(UIFormId.MainForm, this);
        }
        #endregion

        #region Init
        private void InitBinBall()
        {
            m_Id = GameEntry.Entity.GenerateSerialId();
            GameEntry.Entity.ShowBinBall(new BinballData(m_Id, 70003)
            {
                Name = "My BinBall",
                Position = new Vector3(-3.62f, 7f, 0f),
            });
        }

        #endregion
        #region private
        public void SetGameMode(GameMode gameMode)
        {
            if (m_CurrentGame != null)
            {
                m_CurrentGame.Shutdown();
            }
            m_CurrentGame = m_Games[gameMode];
            m_CurrentGame.Initialize(m_MyBall);
            m_MainForm.SetMode(m_CurrentGame.GameMode);
        }

        #endregion
    }
}
