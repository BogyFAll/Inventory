namespace InventorySystem
{
	/// <summary>
	/// Кэшированный инвентарь
	/// </summary>
	/// <typeparam name="TItem">Тип предмета, который хранит инвентарь</typeparam>
	[System.Serializable]
	public class CachInventory<TItem> where TItem : CachItem
	{
		public TItem[] Items;
	}
}
