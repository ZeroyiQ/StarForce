using GameFramework;
using UnityEngine;
using UnityEngine.UI;
using UnityGameFramework.Runtime;

namespace BinBall
{
    public class MainForm : UGuiForm
    {
        [SerializeField]
        private GameObject m_Start = null;
        [SerializeField]
        private Text m_ScoreText = null;
        [SerializeField]
        private Text m_EndPointText = null;
        private GameMode m_Mode;
        private ProcedureMain m_Procedure;
        public void SetScoreText(float score)
        {
            m_ScoreText.text = GameEntry.Localization.GetString("Main.Score",score.ToString("F2"));
        }
        public void SetMode(GameMode mode)
        {
            switch (mode)
            {
                case GameMode.Build:
                    m_ScoreText.gameObject.SetActive(false);
                    m_Start.SetActive(true);

                    break;
                case GameMode.Show:
                    m_ScoreText.gameObject.SetActive(true);
                    m_Start.SetActive(false);

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
        #endregion

        public void OnClickStart()
        {
            if (m_Procedure != null)
            {
                m_Procedure.SetGameMode(GameMode.Show);
            }
        }
    }
}
