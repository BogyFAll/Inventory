using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace InventorySystem
{
	public class InventoryFactory : IInventoryFactory
	{
		private readonly Dictionary<long, ItemStatus> _itemsLoad = new Dictionary<long, ItemStatus>();

		public CachInventory<CachItem> AllInventoryItems { get; private set; }

		public async Task<Item> LoadAndGetItemAsync( long id )
		{
			CachItem item = AllInventoryItems.Items.First( x => x.Id == id );
			Item returnItem = null;

			if ( _itemsLoad.ContainsKey( id ) )
			{
				returnItem = NewItem( id, item.Name, (Sprite)_itemsLoad[id].Handle.Result );

				_itemsLoad[id] = new ItemStatus { Count = _itemsLoad[id].Count + 1, Handle = _itemsLoad[id].Handle };
			}
			else
			{
				AsyncOperationHandle<Sprite> handle = Addressables.LoadAssetAsync<Sprite>( item.ImagePath );
				await handle.Task;

				if ( handle.Status == AsyncOperationStatus.Succeeded )
				{
					returnItem = NewItem( id, item.Name, handle.Result );

					_itemsLoad.Add( returnItem.Id, new ItemStatus { Count = 1, Handle = handle } );
				}
			}

			return await Task.FromResult( returnItem );
		}

		public async Task LoadJsonInventoryAsync( AssetReferenceT<TextAsset> assetReference )
		{
			TextAsset json = await assetReference.LoadAssetAsync().Task;

			AllInventoryItems = JsonUtility.FromJson<CachInventory<CachItem>>( json.text );

			assetReference.ReleaseAsset();

			await Task.CompletedTask;
		}

		public void ReloadItem( long id )
		{
			if ( _itemsLoad.ContainsKey( id ) )
			{
				_itemsLoad[id] = new ItemStatus { Count = _itemsLoad[id].Count - 1, Handle = _itemsLoad[id].Handle };

				if ( _itemsLoad[id].Count <= 0 )
				{
					Addressables.Release( _itemsLoad[id].Handle );
					_itemsLoad.Remove( id );
				}
			}
		}

		private Item NewItem( long id, string name, Sprite sprite )
		{
			return new Item
			{
				Id = id,
				Name = name,
				Sprite = sprite,
				Count = 1
			};
		}

		private struct ItemStatus
		{
			public long Count { get; set; }
			public AsyncOperationHandle Handle { get; set; }
		}
	}
}
