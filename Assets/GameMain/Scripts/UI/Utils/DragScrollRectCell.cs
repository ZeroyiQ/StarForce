using UnityEngine;
using UnityEngine.EventSystems;
using UnityGameFramework.Runtime;
namespace BinBall
{

    public class DragScrollRectCell : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField]
        private int m_ID;
        [SerializeField]
        public GameObject Template;
        public void OnPointerDown(PointerEventData eventData)
        {
            DragScrollRect.Instance.ChooseCell(Template);
            Log.Error("1111");
        }
    }
}