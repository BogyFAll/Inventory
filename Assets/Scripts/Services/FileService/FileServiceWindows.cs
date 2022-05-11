using System;
using System.IO;
using System.Threading.Tasks;

namespace Services
{
	public class FileServiceWindows : FileServiceBase, IFileService
	{
		public FileServiceWindows()
		{
			FolderPath = Path.Combine( Environment.GetFolderPath( Environment.SpecialFolder.MyDocuments ), "InventoryTestGame" );
			MainFileName = "Inventory.dal";
		}

		protected override string FolderPath { get; }
		protected override string MainFileName { get; }

		public override void DeleteMainFile()
		{
			string path = Path.Combine( FolderPath, MainFileName );

			if ( File.Exists( path ) )
				File.Delete( path );
		}

		public async override Task<Stream> LoadFileStreamAsync()
		{
			string path = Path.Combine( FolderPath, MainFileName );

			if ( !Directory.Exists( FolderPath ) )
				Directory.CreateDirectory( FolderPath );

			return await Task.FromResult( new FileStream( path, FileMode.OpenOrCreate, FileAccess.ReadWrite ) );
		}
	}
}