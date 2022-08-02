using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Helix
{
    public class Movement : MonoBehaviour
    {
        public float rotMult;
        private void LateUpdate()
        {
            if (!Inputs.triggerRight) return;

            float pointerDeltaAmount = -Inputs.stickRight.x * rotMult;
            transform.Rotate(transform.up * pointerDeltaAmount);
        }
    }
}
