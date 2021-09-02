using UnityEngine;
using UnityEngine.EventSystems;

namespace BinBall
{
    /// <summary>
    /// 可移动的实体类。
    /// </summary>
    public class MoveObject : Entity
    {
        private Vector3 startPos;
        private Vector3 endPos;
        private Vector3 offset;
        private bool m_OperationUIVisible;
        private RotationUI m_RotationUI;
        private DestoryUI m_DestoryUI;

        public void ChangeRoataion(int value)
        {
            transform.AddLocalRoationZ(value);
        }

        public void DestoryThis()
        {
            GameEntry.Entity.HideEntity(this);
            HideAllUI();
        }

        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
            gameObject.SetLayerRecursively(Constant.Layer.TargetableObjectLayerId);
        }

        protected override void OnShow(object userData)
        {
            base.OnShow(userData);
            m_OperationUIVisible = false;
        }

        private void OnMouseDown()
        {
            if (Verify())
            {
                StarDrag();
            }
        }

        private void OnMouseDrag()
        {
            if (Verify())
            {
                Drag();
            }
        }

        private void OnMouseUp()
        {
            transform.position = ConstraintUtility.GetPositionInLimitArea(transform.position, new Vector4(-6, 6, 6, -6), .5f);
        }

        private void OnMouseUpAsButton()
        {
            if (Verify() && ExitUI())
            {
                ChangeOperationUIVisible();
            }
        }

        private bool Verify()
        {
            var procedure = GameEntry.Procedure.CurrentProcedure;
            if (procedure.GetType() == typeof(ProcedureMain))
            {
                var procedureMain = procedure as ProcedureMain;
                return procedureMain.IsBuildMode();
            }
            return false;
        }

        private void StarDrag(Vector2? position = null)
        {
            startPos = MyScreenPointToWorldPoint(Input.mousePosition, transform);
        }

        private void Drag()
        {
            endPos = MyScreenPointToWorldPoint(Input.mousePosition, transform);
            //计算偏移量
            offset = endPos - startPos;
            //让cube移动
            transform.position += offset;
            //这一次拖拽的终点变成了下一次拖拽的起点
            startPos = endPos;
        }

        private Vector3 MyScreenPointToWorldPoint(Vector3 ScreenPoint, Transform target)
        {
            //1 得到物体在主相机的xx方向
            Vector3 dir = (target.position - Camera.main.transform.position);
            //2 计算投影 (计算单位向量上的法向量)
            Vector3 norVec = Vector3.Project(dir, Camera.main.transform.forward);
            //返回世界空间
            return Camera.main.ViewportToWorldPoint(new Vector3(ScreenPoint.x / Screen.width, ScreenPoint.y / Screen.height, norVec.magnitude));
        }

        private void ChangeOperationUIVisible()
        {
            m_OperationUIVisible = !m_OperationUIVisible;
            if (m_OperationUIVisible)
            {
                m_RotationUI = GameEntry.OperationUI.ShowRotationUI(this);
                m_DestoryUI = GameEntry.OperationUI.ShowDestoryUI(this);
            }
            else
            {
                HideAllUI();
            }
        }

        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(elapseSeconds, realElapseSeconds);
            if (!Verify() && m_OperationUIVisible)
            {
                HideAllUI();
            }
        }

        /// <summary>
        /// 是否离开UI
        /// </summary>
        /// <returns></returns>
        private bool ExitUI()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
        if (!EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
            return true;
#else
            if (!EventSystem.current.IsPointerOverGameObject())
                return true;
#endif
            else
                return false;
        }

        private void HideAllUI()
        {
            if (m_RotationUI != null)
            {
                GameEntry.OperationUI.HideOperationUI(m_RotationUI);
            }
            if (m_DestoryUI != null)
            {
                GameEntry.OperationUI.HideOperationUI(m_DestoryUI);
            }
        }
    }
}