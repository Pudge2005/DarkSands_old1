using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.View
{
    public class UiInputTester : MonoBehaviour, IPointerClickHandler, IBeginDragHandler,
        IEndDragHandler, IDropHandler, IDragHandler
    {
        public void OnBeginDrag(PointerEventData eventData)
        {
            Log(nameof(OnBeginDrag));
        }

        public void OnDrag(PointerEventData eventData)
        {
            return;
            Log(nameof(OnDrag));
        }

        public void OnDrop(PointerEventData eventData)
        {
            Log(nameof(OnDrop));
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            Log(nameof(OnEndDrag));
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            Log(nameof(OnPointerClick));
        }

        private void Log(string msg)
        {
            UnityEngine.Debug.Log($"{name}: {msg}");
        }
    }
}
