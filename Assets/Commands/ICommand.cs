﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

namespace Artemito { 
public interface ICommand
{
    // Start is called before the first frame update
    Task Execute();

    void Skip();
}
}