using System;
using UnityEngine;

namespace UnityScriptLab {
    namespace Input {
        public delegate void InputControlEvent();

        public interface InputControl {
            event Action Triggered;

            void Update();
        }

    }
}
