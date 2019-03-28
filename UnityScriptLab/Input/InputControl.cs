using UnityEngine;

namespace UnityScriptLab {
    namespace Input {
        public delegate void TriggerEvent();

        public interface InputControl {
            event TriggerEvent Triggered;

            void Update();
        }

    }
}
