using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapTrigger : MonoBehaviour
{
	public void TriggerTrap(EnemyBase triggeredByEnemy)
	{
		Trap trap = transform.parent.gameObject.GetComponent<Trap>();
		if (trap)
		{
			trap.TriggerTrap(triggeredByEnemy);
		}
	}
}
