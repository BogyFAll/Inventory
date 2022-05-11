using Services;
using System.Linq;
using UnityEngine;
using Zenject;

namespace InventorySystem.UI
{
	public class InventoryItemButtons : MonoBehaviour
	{
		[SerializeField] InventoryUI _inventoryUI;
		[SerializeField] GameObject _inventoryItems;

		[Inject] private IInventoryDalFacade _inventoryDalFacade;
		[Inject] private IInventoryFactory _inventoryFactory;
		[Inject] private IFileService _fileService;

		/// <summary>
		/// Тестовое добавление случайного предмета из общего списка предметов в инвентарь
		/// </summary>
		public async void AddItem()
		{
			if ( _inventoryUI.IsLoaded ) return;

			_inventoryItems.SetActive( false );

			Random.InitState( Mathf.RoundToInt( Time.time ) );

			for ( int i = 0; i < 95; i++ )
			{
				CachItem cachItem = _inventoryFactory.AllInventoryItems.Items[Random.Range( 0, _inventoryFactory.AllInventoryItems.Items.Length )];

				CachGameItem item = new CachGameItem
				{
					Id = cachItem.Id,
					Name = cachItem.Name,
					Count = Random.Range( 1, 65 ),
					ImagePath = cachItem.ImagePath
				};

				_inventoryDalFacade.AddItemInCurrentInventory( item );

				await _inventoryUI.AddItemAsync( item );
			}

			_inventoryItems.SetActive( true );
		}

		/// <summary>
		/// Сохраняет текущий инвентарь в файл сохранений
		/// </summary>
		public async void SaveItemsInFile()
		{
			if ( _inventoryUI.IsLoaded ) return;

			Debug.Log( "Save" );
			await _inventoryDalFacade.CachInventoryInFileAsync();
		}

		/// <summary>
		/// Удаляет весь игровой инвентарь и удаляет файл сохранений
		/// </summary>
		public void DeleteFile()
		{
			if ( _inventoryUI.IsLoaded ) return;

			Debug.Log( "Delete" );

			_inventoryUI.UpdateIndexWithUI( 1 );

			_inventoryDalFacade.CurrentInventory.Items = Enumerable.Empty<CachGameItem>().ToArray();
			_fileService.DeleteMainFile();

			_inventoryUI.ClearInventory();
		}
	}
}
