using System.Threading.Tasks;

namespace InventorySystem
{
	/// <summary>
	/// Для работы с игровым инвентарем
	/// </summary>
	public interface IInventoryDalFacade
	{
		/// <summary>
		/// Текущий игровой инвентарь
		/// </summary>
		CachInventory<CachGameItem> CurrentInventory { get; }

		/// <summary>
		/// Асинхронно загрузить инвентарь ИЗ файла
		/// </summary>
		Task CachInventoryFormFileAsync();
		/// <summary>
		/// Асинхронно загрузить инвентарь В файл
		/// </summary>
		Task CachInventoryInFileAsync();
		/// <summary>
		/// Асинхронно загрузить сцену UI инвентаря
		/// </summary>
		Task LoadInventorySceneAsync();
		/// <summary>
		/// Асинхронно выгрузить сцену UI инвентаря 
		/// </summary>
		Task ReloadInventorySceneAsync();
		/// <summary>
		/// Добавление предмета в игровой инвентарь
		/// </summary>
		/// <param name="item">Добавляемый предмет</param>
		void AddItemInCurrentInventory( CachGameItem item );
	}
}
