using UnityEngine;
using Zenject;

namespace InventorySystem.UI
{
	public class InventoryListControl : MonoBehaviour
	{
		[SerializeField] InventoryUI _inventoryUI;
		[SerializeField] GameObject _inventoryItems;

		[Inject] private IInventoryDalFacade _inventoryDalFacade;

		/// <summary>
		/// Перейти на другую страницу инвентаря
		/// </summary>
		/// <param name="direction">Указывает, в каком направлении переключить инвентарь</param>
		public async void MoveInventoryPage( int direction )
		{
			if ( _inventoryUI.IsLoaded ) return;

			_inventoryItems.SetActive( false );

			int lateIndex = _inventoryUI.Index;

			_inventoryUI.UpdateIndexWithUI( Mathf.Clamp( _inventoryUI.Index + direction,
							1,
							Mathf.CeilToInt( (float)_inventoryDalFacade.CurrentInventory.Items.Length / _inventoryUI.ItemsUI.Count ) )
						   );

			if ( lateIndex == _inventoryUI.Index )
			{
				_inventoryItems.SetActive( true );
				return;
			}

			_inventoryUI.ClearInventory();
			await _inventoryUI.LoadInventoryByIndexAsync();

			_inventoryItems.SetActive( true );
		}

		/// <summary>
		/// Переход на Первую/Последнюю страницу инвентаря
		/// </summary>
		/// <param name="isMax">Перейти ли на последнюю страницу инвентаря?</param>
		public async void MaxMinPage( bool isMax )
		{
			if ( _inventoryUI.IsLoaded ) return;

			_inventoryItems.SetActive( false );

			int lateIndex = _inventoryUI.Index;

			_inventoryUI.UpdateIndexWithUI( isMax ? Mathf.CeilToInt( (float)_inventoryDalFacade.CurrentInventory.Items.Length / _inventoryUI.ItemsUI.Count ) : 1 );

			if ( lateIndex == _inventoryUI.Index )
			{
				_inventoryItems.SetActive( true );
				return;
			}

			_inventoryUI.ClearInventory();
			await _inventoryUI.LoadInventoryByIndexAsync();

			_inventoryItems.SetActive( true );
		}
	}
}
