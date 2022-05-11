namespace InventorySystem
{
	/// <summary>
	/// ������������ ���������
	/// </summary>
	/// <typeparam name="TItem">��� ��������, ������� ������ ���������</typeparam>
	[System.Serializable]
	public class CachInventory<TItem> where TItem : CachItem
	{
		public TItem[] Items;
	}
}
