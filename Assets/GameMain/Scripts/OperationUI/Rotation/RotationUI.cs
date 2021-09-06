using UnityEngine;

namespace BinBall
{
    public class RotationUI : OperationUI
    {
        [SerializeField]
        private CommonButton down;

        private bool tryToExcute;

        private void Awake()
        {
            down.m_OnDown.AddListener(OnClickDown);
            down.m_OnUp.AddListener(OnClickUp);
            m_ShowOffset = new Vector2(150, 40);
            tryToExcute = false;
        }

        public void OnClickDown()
        {
            if (Owner != null && Owner.GetType().BaseType == typeof(MoveObject))
            {
                tryToExcute = true;
            }
        }

        public void OnClickUp()
        {
            tryToExcute = false;
        }

        private void Update()
        {
            if (tryToExcute && Owner != null && Owner.GetType().BaseType == typeof(MoveObject))
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
                {
                    Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
                    MoveObject obj = Owner as MoveObject;
                    float angle = touchDeltaPosition.x * 3 * Time.deltaTime;
                    obj.ChangeRoataion(angle);
                    down.transform.Rotate(Owner.gameObject.transform.position, angle);
                }
#else
                if (Input.GetMouseButton(0))
                {
                    MoveObject obj = Owner as MoveObject;
                    float angle = Input.GetAxis("Mouse Y") * 3;
                    obj.ChangeRoataion(angle);
                    angle = obj.transform.eulerAngles.z * Mathf.PI / 180;
                    down.transform.localPosition = new Vector3(-150 + 150 * Mathf.Cos(angle), 150 * Mathf.Sin(angle), 0);
                }
#endif
            }
        }
    }
}