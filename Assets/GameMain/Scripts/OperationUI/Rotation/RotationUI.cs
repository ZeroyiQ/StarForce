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

        public void OnClickUp()
        {
            if (Owner != null && Owner.GetType().BaseType == typeof(MoveObject))
            {
                MoveObject obj = Owner as MoveObject;
                obj.ChangeRoataion(30);
            }
        }

        public void OnClickDown()
        {
            if (Owner != null && Owner.GetType().BaseType == typeof(MoveObject))
            {
                MoveObject obj = Owner as MoveObject;
                obj.ChangeRoataion(-30);
            }
        }

    }
}
