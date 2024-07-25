using GameContent.InventorySystem.InventorySystemWithSlots.Abstract;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameContent.InventorySystem.InventorySystemWithSlots.UI
{
    public class UIInventoryItem : UIItem
    {
        [SerializeField] private Image _image;
        [SerializeField] private TextMeshProUGUI _textAmount;
        
        public IInventoryItem Item { get; private set; }

        public void Refresh(IInventorySlot slot)
        {
            if (slot.IsEmpty)
            {
                CleanUp();
                return;
            }

            Item = slot.Item;

            _image.sprite = Item.Info.SpriteIcon;
            _image.gameObject.SetActive(true);

            var textAmountEnabled = slot.Amount > 1;

            _textAmount.gameObject.SetActive(textAmountEnabled);

            if (textAmountEnabled)
            {
                _textAmount.text = slot.Amount.ToString();
            }
        }

        private void CleanUp()
        {
            _image.gameObject.SetActive(false);
            _textAmount.gameObject.SetActive(false);
        }
    }
}