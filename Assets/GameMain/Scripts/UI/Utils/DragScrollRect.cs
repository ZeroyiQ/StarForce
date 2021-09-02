using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace BinBall
{
    public class DragScrollRect : ScrollRect, IPointerExitHandler
    {
        private static DragScrollRect m_Instance;
        public static DragScrollRect Instance { get => m_Instance; }
        public int NowObj;
        private bool isExit = false;

        protected override void Awake()
        {
            base.Awake();
            if (!m_Instance)
            {
                m_Instance = this;
            }
        }

        public void ChooseCell(GameObject gameObject)
        {
            GameEntry.Entity.ShowBuildCube(new BuilderCubeData(GameEntry.Entity.GenerateSerialId(), 70004)
            {
                Position = Vector3.zero,
                LocalScale = new Vector3(5, .5f, 1)
            });
        }

        public override void OnDrag(PointerEventData eventData)
        {
            if (!isExit)
            {
                base.OnDrag(eventData);
            }
        }

        public override void OnEndDrag(PointerEventData eventData)
        {
            base.OnEndDrag(eventData);
        }

        public override void OnBeginDrag(PointerEventData eventData)
        {
            isExit = false;
            base.OnBeginDrag(eventData);
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

        public void OnPointerExit(PointerEventData eventData)
        {
            OnEndDrag(eventData);
        }
    }
}