using Zenject;
using UnityEngine;
using UnityEngine.AddressableAssets;
using System.Threading.Tasks;

namespace InventorySystem
{
	public class InventoryInstaller : MonoInstaller
	{
		[SerializeField] AssetReferenceT<TextAsset> _inventoryListAsset;

		public override void InstallBindings()
		{
			Container.Bind<IInventoryFactory>().To<InventoryFactory>().AsSingle();
			Container.Bind<InventoryInstaller>().FromInstance( this );
		}

		public async Task InitInventory()
		{
			IInventoryFactory factory = Container.Resolve<IInventoryFactory>();

			await factory.LoadJsonInventoryAsync( _inventoryListAsset );
		}
	}
}
