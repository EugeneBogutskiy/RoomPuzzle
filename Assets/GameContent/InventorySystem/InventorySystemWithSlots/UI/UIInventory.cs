using System;
using UnityEngine;

namespace GameContent.InventorySystem.InventorySystemWithSlots.UI
{
    public class UIInventory : MonoBehaviour
    {
        [SerializeField] private int _capacity = 6;
        
        public InventoryWithSlots Inventory { get; private set; }

        private void Awake()
        {
            Inventory = new InventoryWithSlots(_capacity);
        }
    }
}