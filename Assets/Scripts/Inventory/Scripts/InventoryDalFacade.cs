using Services;
using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;

namespace InventorySystem
{
	public class InventoryDalFacade : IInventoryDalFacade
	{
		#region Fields

		private readonly IFileService _fileService;

		private AsyncOperationHandle<SceneInstance> _currentScene;

		#endregion

		#region .ctr

		public InventoryDalFacade( IFileService fileService )
		{
			_fileService = fileService;
		}

		#endregion

		#region Properties

		public CachInventory<CachGameItem> CurrentInventory { get; private set; }

		#endregion

		#region Methods

		public async Task CachInventoryFormFileAsync()
		{
			BinaryFormatter formatter = new BinaryFormatter();
			Stream stream = await _fileService.LoadFileStreamAsync();

			if ( stream.Length != 0 )
				CurrentInventory = (CachInventory<CachGameItem>)formatter.Deserialize( stream );
			else
				CurrentInventory = new CachInventory<CachGameItem>() { Items = Enumerable.Empty<CachGameItem>().ToArray() };

			stream.Close();

			await Task.CompletedTask;
		}

		public async Task CachInventoryInFileAsync()
		{
			BinaryFormatter formatter = new BinaryFormatter();
			var stream = await _fileService.LoadFileStreamAsync();

			formatter.Serialize( stream, CurrentInventory );
			stream.Close();
		}

		public void AddItemInCurrentInventory( CachGameItem item )
		{
			Array.Resize( ref CurrentInventory.Items, CurrentInventory.Items.Length + 1 );
			CurrentInventory.Items[CurrentInventory.Items.Length - 1] = item;
		}

		public async Task LoadInventorySceneAsync()
		{
			if ( !_currentScene.IsValid() )
			{
				_currentScene = Addressables.LoadSceneAsync( "InventoryScene", UnityEngine.SceneManagement.LoadSceneMode.Additive );
				await _currentScene.Task;
			}

			await Task.CompletedTask;
		}

		public async Task ReloadInventorySceneAsync()
		{
			if ( _currentScene.IsValid() )
				await Addressables.UnloadSceneAsync(_currentScene).Task;

			await Task.CompletedTask;
		}

		#endregion
	}
}
