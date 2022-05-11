using UnityEngine;

namespace InventorySystem
{
	/// <summary>
	/// ���������� ��������� ��������
	/// </summary>
	public class Item
	{
		public long Id { get; set; }
		public string Name { get; set; }
		public Sprite Sprite { get; set; }
		public int Count { get; set; }
	}
}
