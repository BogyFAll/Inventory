using Services;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

public class LoadScreenInstaller : MonoInstaller
{
	[SerializeField] AssetReference _loadScreenScene;

	public override void InstallBindings()
	{
		ILoadScreenService screenService = new LoadScreenService();
		screenService.LoadScreenScene = _loadScreenScene;

		Container.Bind<ILoadScreenService>().FromInstance( screenService ).AsSingle();
	}
}