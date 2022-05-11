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
		/// ������� ����� ���������
		/// </summary>
		public int Index { get => _index; }
		/// <summary>
		/// ���������, ���� �� �������� ��������� � ���������
		/// </summary>
		public bool IsLoaded { get => _isLoad; }
		/// <summary>
		/// ������ ���� ����� ���������
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
		/// ��������� ����� ������ �������� ��������� � ��������� �������� �� UI (��� �������� ������ ���������)
		/// </summary>
		/// <param name="index">����� ������</param>
		public void UpdateIndexWithUI( int index )
		{
			_index = index;
			_indexText.text = _index.ToString();
		}

		/// <summary>
		/// ������� ��������� � ��������� ��� �������� � �������
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
		/// ��������� ���������� ������� ��������� �� �������� �������� �������
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
		/// ��������� ������� � ��������� ������ ������, ���� ��� �������
		/// </summary>
		/// <param name="cachItem">����������� ������� � ������</param>
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
		/// ��������� ���������. ��� ���� ��������� ��������� ��� ��������, �������� ���������� ������� OnCloseInventoryAction � ��������� ����� ���������
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
