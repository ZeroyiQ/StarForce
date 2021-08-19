using GameFramework;
using UnityEngine;
using UnityEngine.UI;
using UnityGameFramework.Runtime;

namespace BinBall
{
    public class MainForm : UGuiForm
    {
        [SerializeField]
        private Text m_ScoreText = null;
        [SerializeField]
        private Text m_EndPointText = null;
    }
}
