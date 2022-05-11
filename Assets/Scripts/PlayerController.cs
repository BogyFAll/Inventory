using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;
using System.Threading.Tasks;
using InventorySystem;
using Services;

public class PlayerController : MonoBehaviour
{
	[SerializeField] GameObject _ui;

	[Inject] private IInventoryDalFacade _inventoryDalFacade;
	[Inject] private IGlobalAction _globalAction;

	private void OnEnable()
	{
		_globalAction.OnCloseInventoryAction += ActiveUI;
	}

	private void OnDisable()
	{
		_globalAction.OnCloseInventoryAction -= ActiveUI;
	}

	private void ActiveUI() => _ui.SetActive( true );

	private async Task OpenInventotyAsync()
	{
		_ui.SetActive( false );

		_globalAction.InvokeOpenOrCloseInventoryAction( true );

		await _inventoryDalFacade.LoadInventorySceneAsync();
	}

	public async void Key_Pause( InputAction.CallbackContext context )
	{
		if ( context.started )
			await OpenInventotyAsync();
	}

	public async void Click()
	{
		await OpenInventotyAsync();
	}
}