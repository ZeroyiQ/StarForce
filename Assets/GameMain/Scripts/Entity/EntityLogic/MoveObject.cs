//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using UnityEngine;
using UnityGameFramework.Runtime;

namespace BinBall
{
    /// <summary>
    /// 可移动的实体类。
    /// </summary>
    public class MoveObject : Entity
    {

        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
            gameObject.SetLayerRecursively(Constant.Layer.TargetableObjectLayerId);
        }

        protected override void OnShow(object userData)
        {
            base.OnShow(userData);
        }


        //用于 实现可以拖动物体移动 
        //先写一个方法  将屏幕坐标转换成世界装备
        //或表达为 将屏幕空间转化为世界空间 

        Vector3 MyScreenPointToWorldPoint(Vector3 ScreenPoint, Transform target)
        {
            //1 得到物体在主相机的xx方向
            Vector3 dir = (target.position - Camera.main.transform.position);
            //2 计算投影 (计算单位向量上的法向量)
            Vector3 norVec = Vector3.Project(dir, Camera.main.transform.forward);
            //返回世界空间
            return Camera.main.ViewportToWorldPoint
                (
                   new Vector3(
                       ScreenPoint.x / Screen.width,
                       ScreenPoint.y / Screen.height,
                       norVec.magnitude
                   )
                );

        }

        Vector3 startPos;
        Vector3 endPos;
        Vector3 offset;

        private void OnMouseDown()
        {
            StarDrag();
        }

        private void OnMouseDrag()
        {
            Drag();
        }

        public void StarDrag(Vector2? position = null)
        {
            if (position != null)
            {
                // transform.position = new Vector3(position.x, position.y, 0);
            }
            startPos = MyScreenPointToWorldPoint(Input.mousePosition, transform);
        }
        public void Drag()
        {
            endPos = MyScreenPointToWorldPoint(Input.mousePosition, transform);
            //计算偏移量
            offset = endPos - startPos;
            //让cube移动
            transform.position += offset;
            //这一次拖拽的终点变成了下一次拖拽的起点
            startPos = endPos;
        }
    }
}
