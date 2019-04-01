using System;

using UnityEngine;

namespace UnityScriptLab {
    namespace Input {
        public interface InputControl {
            event Action Triggered;

            void HandleInput();
        }

    }
}
