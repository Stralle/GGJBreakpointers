using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EDestructibleType
{
	Table,
	Stone,
	DeadBody
}

public interface IDestructible
{
	EDestructibleType GetDestructibleType();
	void DestroyAndGetResources();
}
