using System.Threading.Tasks;
using UnityEngine.AddressableAssets;

namespace Services
{
	/// <summary>
	/// Для работы с окном загрузки
	/// </summary>
	public interface ILoadScreenService
	{
		/// <summary>
		/// Указатель на сцену загрузки
		/// </summary>
		AssetReference LoadScreenScene { get; set; }
		/// <summary>
		/// Добавляет сцену загрузки
		/// </summary>
		Task AddSceenLoadAsync();
		/// <summary>
		/// Выгружает сцену загрузки
		/// </summary>
		Task ReloadScreenLoadAsync();
	}
}
