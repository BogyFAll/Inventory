using System;

namespace Services
{
	/// <summary>
	/// Глобальные игровые события
	/// </summary>
	public interface IGlobalAction
	{
		event Action OnOpenInventoryAction;
		event Action OnCloseInventoryAction;

		public void InvokeOpenOrCloseInventoryAction( bool isOpen );
	}
}
