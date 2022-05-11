using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Services
{
	public class LoadScreenService : ILoadScreenService
	{
		public AssetReference LoadScreenScene { get; set; }

		public async Task AddSceenLoadAsync()
		{
			await LoadScreenScene.LoadSceneAsync( UnityEngine.SceneManagement.LoadSceneMode.Additive ).Task;
		}

		public async Task ReloadScreenLoadAsync()
		{
			await LoadScreenScene.UnLoadScene().Task;
			LoadScreenScene.ReleaseAsset();

			await Task.CompletedTask;
		}
	}
}
