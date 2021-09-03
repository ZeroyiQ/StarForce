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

        public void TryCreateBuilder(BuilderType builder,Vector3 screenPosition)
        {
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPosition);
            switch (builder)
            {
                case BuilderType.Cube:
                    GameEntry.Entity.ShowBuildCube(new BuilderCubeData(GameEntry.Entity.GenerateSerialId(), 70004)
                    {
                        Position = new Vector3(worldPos.x, worldPos.y, 0),
                        LocalScale = new Vector3(5, .5f, 1)
                    });
                    break;
                case BuilderType.BombBoard:
                    break;
            }
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


        public float _zDistance = 50f;
        //对象的缩放系数
        public float _scaleFactor = 1.2f;
        //地面层级
        public LayerMask _groundLayerMask;
        int touchID;
        bool isDragging = false;
        bool isTouchInput = false;
        //是否是有效的放置（如果放置在地面上返回True,否则为False）
        bool isPlaceSuccess = false;
        //当前要被放置的对象
        public GameObject currentPlaceObj = null;
        private int currentDragEntity = 0;
        //坐标在Y轴上的偏移量
        public float _YOffset = 0.5F;

        void Update()
        {
            if (currentDragEntity ==0) return;

            if (CheckUserInput())
            {
                MoveCurrentPlaceObj();
            }
            else if (ExitUI())
            {
                CheckIfPlaceSuccess();
            }
        }
        /// <summary>
        ///检测用户当前输入
        /// </summary>
        /// <returns></returns>
        bool CheckUserInput()
        {
#if !UNITY_EDITOR && (UNITY_ANDROID || UNITY_IOS)
        if (Input.touches.Length > ) {
            if (!isTouchInput) {
                isTouchInput = true;
                touchID = Input.touches[].fingerId;
                return true;
            } else if (Input.GetTouch (touchID).phase == TouchPhase.Ended) {
                isTouchInput = false;
                return false;
            } else {
                return true;
            }
        }
        return false;
#else
            return Input.GetMouseButton(0);
#endif
        }
        /// <summary>
        ///让当前对象跟随鼠标移动
        /// </summary>
        void MoveCurrentPlaceObj()
        {
            isDragging = true;
            Vector3 point;
            Vector3 screenPosition;
            isPlaceSuccess = ExitUI();
            currentPlaceObj.transform.position = point + new Vector3(, _YOffset,);
            currentPlaceObj.transform.localEulerAngles = new Vector3(, 60, );
        }
        /// <summary>
        ///在指定位置化一个对象
        /// </summary>
        void CreatePlaceObj(BuilderType builder, Vector3 screenPosition)
        {
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPosition);
            currentDragEntity = GameEntry.Entity.GenerateSerialId();
            switch (builder)
            {
                case BuilderType.Cube:
                    GameEntry.Entity.ShowBuildCube(new BuilderCubeData(currentDragEntity, 70004)
                    {
                        Position = new Vector3(worldPos.x, worldPos.y, 0),
                        LocalScale = new Vector3(5, .5f, 1)
                    });
                    break;
                case BuilderType.BombBoard:
                    break;
            }
        }
        /// <summary>
        ///检测是否放置成功
        /// </summary>
        void CheckIfPlaceSuccess()
        {
            if (isPlaceSuccess)
            {
                CreatePlaceObj();
            }
            isDragging = false;
            currentPlaceObj.SetActive(false);
            currentPlaceObj = null;
        }
        /// <summary>
        /// 将要创建的对象传递给当前对象管理器
        /// </summary>
        /// <param name="newObject"></param>
        public void AttachNewObject(GameObject newObject)
        {
            if (currentPlaceObj)
            {
                currentPlaceObj.SetActive(false);
            }
            currentPlaceObj = newObject;
        }
    }
}
}