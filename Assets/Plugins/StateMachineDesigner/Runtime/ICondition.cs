using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateMachineDesigner.Runtime
{
    public interface ICondition
    {
        bool Test();
    }
}
