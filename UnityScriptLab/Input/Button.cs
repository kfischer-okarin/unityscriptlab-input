using System;

using UnityEngine;

namespace UnityScriptLab {
    namespace Input {
        public class Button : InputControl {
            KeyCode key;
            public Button(KeyCode key) {
                this.key = key;
                Pressed();
            }

            public Button Pressed() {
                this.TriggerCondition = () => UnityEngine.Input.GetKeyDown(key);
                return this;
            }
            public Button Released() {
                this.TriggerCondition = () => UnityEngine.Input.GetKeyUp(key);
                return this;
            }

            public event Action Triggered;

            public void Update() {
                if (TriggerCondition()) {
                    Triggered?.Invoke();
                }
            }

            Func<bool> TriggerCondition;
        }
    }
}
