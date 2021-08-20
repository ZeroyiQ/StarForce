using UnityEngine;
using UnityEngine.EventSystems;
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
        }
    }
}