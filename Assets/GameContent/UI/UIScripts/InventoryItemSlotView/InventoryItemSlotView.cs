using GameContent.InventorySystem.SimpleInventorySystem;
using GameContent.UI.UIScripts.InventoryItemSlotView.Abstract;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemSlotView : MonoBehaviour, IInventoryItemSlotView
{
    [SerializeField]
    private Image _slotBackground;
    [SerializeField]
    private Image _icon;
    [SerializeField]
    private TextMeshProUGUI _itemCount;

    public void Init(InventoryItem item)
    {
        _icon.sprite = item.Data.icon;
        _itemCount.text = item.StackSize.ToString();
    }
}