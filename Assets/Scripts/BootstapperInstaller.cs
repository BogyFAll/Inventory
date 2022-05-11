using InventorySystem;
using Services;
using UnityEngine;
using Zenject;

public class BootstapperInstaller : MonoInstaller
{
	public override void InstallBindings()
	{
		AddFileService();

		Container.Bind<IInventoryDalFacade>().To<InventoryDalFacade>().AsSingle();
		Container.Bind<IGlobalAction>().To<GlobalAction>().AsSingle();
	}

	private void AddFileService()
	{
		switch ( Application.platform )
		{
			case RuntimePlatform.WindowsPlayer:
			case RuntimePlatform.WindowsEditor:
			case RuntimePlatform.WindowsServer:
				Container.Bind<IFileService>().To<FileServiceWindows>().AsTransient();
				break;
			case RuntimePlatform.Android:
				Container.Bind<IFileService>().To<FileServiceAndroid>().AsTransient();
				break;
		}
	}
}
