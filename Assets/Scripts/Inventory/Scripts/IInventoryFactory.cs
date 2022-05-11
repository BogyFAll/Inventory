using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace InventorySystem
{
	/// <summary>
	/// Для хранение, загрузки и выгрузки игровых закэшированных предметов
	/// </summary>
	public interface IInventoryFactory
	{
		/// <summary>
		/// Список всех предметов
		/// </summary>
		CachInventory<CachItem> AllInventoryItems { get; }
		/// <summary>
		/// Асинхронно загрузить список предмета из файла в формате JSON
		/// </summary>
		/// <param name="assetReference">Указатель на JSON</param>
		Task LoadJsonInventoryAsync( AssetReferenceT<TextAsset> assetReference );
		/// <summary>
		/// Асинхронно загружает предмет, если он уже загружен, просто передаст ссылку.
		/// </summary>
		/// <param name="id">Id загружаемого предмета</param>
		/// <returns>Предмет</returns>
		Task<Item> LoadAndGetItemAsync( long id );
		/// <summary>
		/// Выгрузить предмет. Если на него остаются ссылки, то уменьшает их количество на 1
		/// </summary>
		/// <param name="id">Id предмета</param>
		void ReloadItem( long id );
	}
}
