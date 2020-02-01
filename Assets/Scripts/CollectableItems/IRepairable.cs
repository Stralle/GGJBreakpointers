﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRepairable
{
    ETrapType GetTrapType();
    void RepairAndSpendResources();
}
