using System;

namespace Services
{
	public class GlobalAction : IGlobalAction
	{
		public event Action OnOpenInventoryAction;
		public event Action OnCloseInventoryAction;

		public void InvokeOpenOrCloseInventoryAction( bool isOpen = true )
		{
			if ( isOpen )
				OnOpenInventoryAction?.Invoke();
			else
				OnCloseInventoryAction?.Invoke();
		}
	}
}
