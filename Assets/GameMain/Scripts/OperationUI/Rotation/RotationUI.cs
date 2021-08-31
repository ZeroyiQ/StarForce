using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityGameFramework.Runtime;

namespace BinBall
{
    public class RotationUI : OperationUI
    {
        [SerializeField]
        private Button Up;
        [SerializeField]
        private Button down;

        private void Awake()
        {
            Up.onClick.AddListener(OnClickUp);
            down.onClick.AddListener(OnClickDown);
        }

        private void OnClickUp()
        {
            if (Owner != null && Owner.GetType()== typeof(MoveObject))
            {
                MoveObject obj = Owner as MoveObject;
            }
        }

        private void OnClickDown()
        {
            if (Owner != null && Owner.GetType() == typeof(MoveObject))
            {
                MoveObject obj = Owner as MoveObject;
            }
        }

    }
}
