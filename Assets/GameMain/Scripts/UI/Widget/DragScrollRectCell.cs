using UnityEngine;
using UnityEngine.EventSystems;
using UnityGameFramework.Runtime;
namespace BinBall
{

    public class DragScrollRectCell : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField]
        private BuilderType m_Builder;
        private bool tryToCreate = false;



        public void OnPointerDown(PointerEventData eventData)
        {
            DragScrollRect.Instance.TryCreateBuilder(m_Builder, Input.mousePosition);
        }
    }
}