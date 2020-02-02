using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using TMPro;


public class InGameCharacterNotifications : MonoBehaviour
{
	[SerializeField] TextMeshProUGUI _amountOfResources = null;
	[SerializeField] GameObject _tooltipBreak = null;
	[SerializeField] GameObject _tooltipRepare = null;

	// Start is called before the first frame update
	void Start()
	{
		GameRulesManager.Instance.OnTrapLost += HideTooltip;
		GameRulesManager.Instance.OnNewTrapLocated += UpdateComponents;

		HideTooltip();
	}

	void HideTooltip()
	{
		_tooltipBreak.SetActive(false);
		_tooltipRepare.SetActive(false);
		_amountOfResources.gameObject.SetActive(false);
	}

	void UpdateComponents()
	{
		HideTooltip();
		Trap trap = GameRulesManager.Instance.ActiveTrap;
		if (trap)
		{
			if (!trap.IsRepaired)
			{
				_tooltipRepare.SetActive(true);
			}
			else
			{
				_tooltipBreak.SetActive(true);
			}
			_amountOfResources.text = trap.JunkCost.ToString();
			_amountOfResources.gameObject.SetActive(true);
		}
	}

	// Update is called once per frame
	void Update()
	{

	}
}
