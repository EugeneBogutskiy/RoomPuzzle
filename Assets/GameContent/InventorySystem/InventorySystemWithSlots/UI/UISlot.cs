using UnityEngine;
using UnityEngine.EventSystems;

namespace GameContent.InventorySystem.InventorySystemWithSlots.UI
{
    public class UISlot : MonoBehaviour, IDropHandler
    {
        public virtual void OnDrop(PointerEventData eventData)
        {
            var otherTransform = eventData.pointerDrag.transform;
            otherTransform.SetParent(transform);
            otherTransform.localPosition = Vector3.zero;;
        }
    }
}