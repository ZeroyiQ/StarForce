//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using UnityEngine;

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
        private bool m_ShowRotationOp;
        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
            gameObject.SetLayerRecursively(Constant.Layer.TargetableObjectLayerId);
        }

        protected override void OnShow(object userData)
        {
            base.OnShow(userData);
            m_ShowRotationOp = false;
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
            if (Verify())
            {
                ChangeRotationMode();
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
        private void ChangeRotationMode()
        {
            m_ShowRotationOp = !m_ShowRotationOp;
            // TODO 旋转 UI
        }
    }
}