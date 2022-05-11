using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace InventorySystem
{
	/// <summary>
	/// ��� ��������, �������� � �������� ������� �������������� ���������
	/// </summary>
	public interface IInventoryFactory
	{
		/// <summary>
		/// ������ ���� ���������
		/// </summary>
		CachInventory<CachItem> AllInventoryItems { get; }
		/// <summary>
		/// ���������� ��������� ������ �������� �� ����� � ������� JSON
		/// </summary>
		/// <param name="assetReference">��������� �� JSON</param>
		Task LoadJsonInventoryAsync( AssetReferenceT<TextAsset> assetReference );
		/// <summary>
		/// ���������� ��������� �������, ���� �� ��� ��������, ������ �������� ������.
		/// </summary>
		/// <param name="id">Id ������������ ��������</param>
		/// <returns>�������</returns>
		Task<Item> LoadAndGetItemAsync( long id );
		/// <summary>
		/// ��������� �������. ���� �� ���� �������� ������, �� ��������� �� ���������� �� 1
		/// </summary>
		/// <param name="id">Id ��������</param>
		void ReloadItem( long id );
	}
}
