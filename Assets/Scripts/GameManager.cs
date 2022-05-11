using InventorySystem;
using Services;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;
using Zenject;
using System.Threading.Tasks;

public class GameManager : MonoInstaller
{
	[SerializeField] AssetReference _gameScene;

	public async override void InstallBindings()
	{
		ILoadScreenService screenService = Container.Resolve<ILoadScreenService>();

		InventoryInstaller inventoryInstaller = Container.Resolve<InventoryInstaller>();
		IInventoryDalFacade facade = Container.Resolve<IInventoryDalFacade>();

		//��������� ���� ��������
		await screenService.AddSceenLoadAsync();

		//��������� ���������
		await inventoryInstaller.InitInventory();
		await facade.CachInventoryFormFileAsync();

		await _gameScene.LoadSceneAsync( LoadSceneMode.Additive ).Task;
		SceneManager.UnloadSceneAsync( 0 );

		//�������� ���� ��������
		await screenService.ReloadScreenLoadAsync();
	}
}