using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace InventorySystem.UI
{
	/// <summary>
	/// Отображение предмета в UI
	/// </summary>
	public class ItemUI : MonoBehaviour
	{
		[SerializeField] Sprite _defaultBackSprite;

		[Space]
		[SerializeField] TextMeshProUGUI _nameItemText;
		[SerializeField] TextMeshProUGUI _countItemText;
		[SerializeField] Image _mainImage;

		public Item Item { get; private set; }

		/// <summary>
		/// Визуализироват предмет в UI
		/// </summary>
		public void SetItem( Item item )
		{
			Item = item;

			_nameItemText.text = item.Name;
			_countItemText.text = item.Count.ToString();
			_mainImage.sprite = item.Sprite;
		}

		/// <summary>
		/// Убирает все данные об предмете с UI
		/// </summary>
		public void ResetItem()
		{
			Item = null;

			_nameItemText.text = string.Empty;
			_countItemText.text = string.Empty;
			_mainImage.sprite = _defaultBackSprite;
		}
	}
}
