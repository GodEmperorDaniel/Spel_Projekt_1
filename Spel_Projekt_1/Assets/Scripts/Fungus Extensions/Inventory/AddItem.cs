﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fungus {
	[CommandInfo("Inventory",
				 "Add Item",
				 "Adds an item to the player's inventory.")]
	[AddComponentMenu("")]
	public class AddItem : Command {
		[Tooltip("Item to add")]
		[SerializeField] protected InventoryItemData item;

		protected Inventory _inventory;
		public Inventory Inventory {
			get {
				if (_inventory == null) {
					if (PlayerStatic.inventoryInstance != null) {
						_inventory = PlayerStatic.inventoryInstance;
					}
				}

				return _inventory;
			}
		}

		public override void OnEnter() {
			base.OnEnter();

			if (item.Value != null) {
				Inventory.GiftItem(item.Value);
			}

			Continue();
		}

		public override string GetSummary() {
			if (item.Value == null) {
				return "Error: No item selected";
			}

			return item.Value.title;
		}
	}
}