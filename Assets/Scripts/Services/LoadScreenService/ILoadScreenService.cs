using System.Threading.Tasks;
using UnityEngine.AddressableAssets;

namespace Services
{
	/// <summary>
	/// ��� ������ � ����� ��������
	/// </summary>
	public interface ILoadScreenService
	{
		/// <summary>
		/// ��������� �� ����� ��������
		/// </summary>
		AssetReference LoadScreenScene { get; set; }
		/// <summary>
		/// ��������� ����� ��������
		/// </summary>
		Task AddSceenLoadAsync();
		/// <summary>
		/// ��������� ����� ��������
		/// </summary>
		Task ReloadScreenLoadAsync();
	}
}
