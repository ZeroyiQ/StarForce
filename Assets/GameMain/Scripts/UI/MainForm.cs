using UnityEngine;
using UnityEngine.UI;
using UnityGameFramework.Runtime;

namespace BinBall
{
    public class MainForm : UGuiForm
    {
        [SerializeField]
        private GameObject m_Close = null;

        [SerializeField]
        private GameObject m_Show = null;

        [SerializeField]
        private GameObject m_Build = null;

        [SerializeField]
        private Text m_ScoreText = null;

        [SerializeField]
        private Text m_EndPointText = null;

        private GameMode m_Mode;
        private ProcedureMain m_Procedure;

        public void SetScoreText(float score)
        {
            m_ScoreText.text = GameEntry.Localization.GetString("Main.Score", score.ToString("F2"));
        }

        public void SetMode(GameMode mode)
        {
            m_Close.SetActive(true);
            switch (mode)
            {
                case GameMode.Build:
                    m_Build.SetActive(true);
                    m_Show.SetActive(false);
                    break;

                case GameMode.Show:
                    m_Show.SetActive(true);
                    m_Build.SetActive(false);
                    break;
            }
        }

        #region Life Cycle

        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
        }

        protected override void OnOpen(object userData)

        {
            base.OnOpen(userData);

            m_Procedure = (ProcedureMain)userData;
            if (m_Procedure == null)
            {
                Log.Warning("ProcedureMain is invalid when open MenuForm.");
                return;
            }
        }

        #endregion Life Cycle

        public void OnClickStart()
        {
            if (m_Procedure != null)
            {
                m_Procedure.SetGameMode(GameMode.Show);
            }
        }

        public void OnClickBackToBuild()
        {
            if (m_Procedure != null)
            {
                m_Procedure.SetGameMode(GameMode.Build);
            }
        }

        public void OnClickGoMenu()
        {
            if (m_Procedure != null)
            {
                m_Procedure.GotoMenu(true);
            }
        }
    }
}