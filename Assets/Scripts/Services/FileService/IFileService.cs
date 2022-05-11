using System.IO;
using System.Threading.Tasks;

namespace Services
{
	/// <summary>
	/// Для работы с файловой системой
	/// </summary>
	public interface IFileService
	{
		/// <summary>
		/// Асинхронно загружает файл сохранений
		/// </summary>
		/// <returns></returns>
		Task<Stream> LoadFileStreamAsync();
		/// <summary>
		/// Удаляет файл сохранений
		/// </summary>
		void DeleteMainFile();
	}
}