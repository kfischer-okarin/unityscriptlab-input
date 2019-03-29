using UnityEngine;

namespace UnityScriptLab {
    namespace Input {
        public delegate void InputControlEvent();

        public interface InputControl {
            event InputControlEvent Triggered;

            void Update();
        }

    }
}
