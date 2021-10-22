using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System;

namespace PlatformerMVC
{
    public class AIPatrolPath : AIPath
    {
        public event EventHandler TargetReached;

        public override void OnTargetReached()
        {
            base.OnTargetReached();
            DispatchTargetReached();
        }

        protected virtual void DispatchTargetReached()
        {
            TargetReached?.Invoke(this, EventArgs.Empty);
        }
    }
}