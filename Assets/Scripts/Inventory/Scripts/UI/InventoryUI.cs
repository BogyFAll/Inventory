using Services;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using System.Threading.Tasks;
using TMPro;

namespace InventorySystem.UI
{
	public class InventoryUI : MonoBehaviour
	{
		#region Fields

		[SerializeField] TextMeshProUGUI _indexText;
		[SerializeField] Transform _inventoryTransform;
		[SerializeField] List<ItemUI> _items = new List<ItemUI>();

		[Inject] private IInventoryDalFacade _inventoryDalFacade;
		[Inject] private IInventoryFactory _inventoryFactory;
		[Inject] private IGlobalAction _globalAction;

		private int _index;
		private int _maxIndex;
		private bool _isLoad;

		#endregion

		#region Properies

		/// <summary>
		/// Текущий номер инвентаря
		/// </summary>
		public int Index { get => _index; }
		/// <summary>
		/// Проверяет, идет ли загрузка предметов в инвентарь
		/// </summary>
		public bool IsLoaded { get => _isLoad; }
		/// <summary>
		/// Список всех ячеек инвентаря
		/// </summary>
		public IList<ItemUI> ItemsUI { get => _items; }

		#endregion

		private async void Start()
		{
			UpdateIndexWithUI( 1 );

			_inventoryTransform.gameObject.SetActive( false );

			ClearInventory();
			await LoadInventoryByIndexAsync();

			_inventoryTransform.gameObject.SetActive( true );
		}

		/// <summary>
		/// Указывает новый индекс страницы инвентаря и обнавляет значение на UI (без загрузки самого инвентаря)
		/// </summary>
		/// <param name="index">Новый индекс</param>
		public void UpdateIndexWithUI( int index )
		{
			_index = index;
			_indexText.text = _index.ToString();
		}

		/// <summary>
		/// Очищает инвентарь и выгружает все предметы в ячейках
		/// </summary>
		public void ClearInventory()
		{
			foreach ( var item in _items.Where( e => e.Item != null ) )
			{
				_inventoryFactory.ReloadItem( item.Item.Id );
				item.ResetItem();
			}
		}

		/// <summary>
		/// Загружает асинхронно игровой инвентарь по текущему значению индекса
		/// </summary>
		/// <returns></returns>
		public async Task LoadInventoryByIndexAsync()
		{
			_isLoad = true;

			var itemsByCount = _inventoryDalFacade.CurrentInventory.Items
												  .Take( _items.Count * _index )
												  .Skip( _items.Count * ( _index - 1 ) )
												  .ToArray();

			foreach ( var item in itemsByCount )
				await AddItemAsync( item );

			_isLoad = false;
		}

		/// <summary>
		/// Загружает предмет в последнюю пустую ячейку, если она имеется
		/// </summary>
		/// <param name="cachItem">Добавляемый предмет в ячейку</param>
		/// <returns></returns>
		public async Task AddItemAsync( CachGameItem cachItem )
		{
			ItemUI itemUI = ItemsUI.FirstOrDefault( e => e.Item == null );

			if ( itemUI != null )
			{
				Item item = await _inventoryFactory.LoadAndGetItemAsync( cachItem.Id );
				item.Count = cachItem.Count;
				itemUI.SetItem( item );
			}
		}

		/// <summary>
		/// Закрывает инвентарь. При этом полностью выгружает все предметы, вызывает Глобальное событие OnCloseInventoryAction и закрывает сцену инвентаря
		/// </summary>
		public async void CloseInventory()
		{
			if ( _isLoad ) return;

			ClearInventory();

			_globalAction.InvokeOpenOrCloseInventoryAction( false );

			await _inventoryDalFacade.ReloadInventorySceneAsync();
		}
	}
}
