using System.IO;
using System.Threading.Tasks;
using UnityEngine;

namespace Services
{
	public class FileServiceAndroid : FileServiceBase, IFileService
	{
		public FileServiceAndroid()
		{
			FolderPath = Application.persistentDataPath;
			MainFileName = "Inventory.txt";
		}

		protected override string FolderPath { get; }
		protected override string MainFileName { get; }

		public override void DeleteMainFile()
		{
			File.Delete( Path.Combine( FolderPath, MainFileName ) );
		}

		public async override Task<Stream> LoadFileStreamAsync()
		{
			return await Task.FromResult( new FileStream( Path.Combine( FolderPath, MainFileName ), FileMode.OpenOrCreate, FileAccess.ReadWrite ) );
		}
	}
}
