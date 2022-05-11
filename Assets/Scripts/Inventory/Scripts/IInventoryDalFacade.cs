using System.Threading.Tasks;

namespace InventorySystem
{
	/// <summary>
	/// ��� ������ � ������� ����������
	/// </summary>
	public interface IInventoryDalFacade
	{
		/// <summary>
		/// ������� ������� ���������
		/// </summary>
		CachInventory<CachGameItem> CurrentInventory { get; }

		/// <summary>
		/// ���������� ��������� ��������� �� �����
		/// </summary>
		Task CachInventoryFormFileAsync();
		/// <summary>
		/// ���������� ��������� ��������� � ����
		/// </summary>
		Task CachInventoryInFileAsync();
		/// <summary>
		/// ���������� ��������� ����� UI ���������
		/// </summary>
		Task LoadInventorySceneAsync();
		/// <summary>
		/// ���������� ��������� ����� UI ��������� 
		/// </summary>
		Task ReloadInventorySceneAsync();
		/// <summary>
		/// ���������� �������� � ������� ���������
		/// </summary>
		/// <param name="item">����������� �������</param>
		void AddItemInCurrentInventory( CachGameItem item );
	}
}
