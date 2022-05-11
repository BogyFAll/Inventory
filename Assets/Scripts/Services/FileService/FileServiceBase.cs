using System.IO;
using System.Threading.Tasks;

namespace Services
{
	/// <summary>
	/// Базовый класс для файловой системы для разных платформ
	/// </summary>
	public abstract class FileServiceBase : IFileService
	{
		protected abstract string FolderPath { get; }
		protected abstract string MainFileName { get; }

		public abstract void DeleteMainFile();
		public abstract Task<Stream> LoadFileStreamAsync();
	}
}
