using System;

using UnityEngine;

namespace UnityScriptLab {
    namespace Input {
        public class Button : InputControl {
            KeyCode key;
            Func<bool> TriggerCondition;
            public Button(KeyCode key) {
                this.key = key;
                this.TriggerCondition = () => false;
            }

            public Button Press {
                get {
                    this.TriggerCondition = () => UnityEngine.Input.GetKeyDown(key);
                    return this;
                }
            }

            public Button Release {
                get {
                    this.TriggerCondition = () => UnityEngine.Input.GetKeyUp(key);
                    return this;
                }
            }

            public Button And(Button other) {
                this.TriggerCondition = () => UnityEngine.Input.GetKey(key) && other.TriggerCondition();
                return this;
            }

            public event Action Triggered;

            public void Update() {
                if (TriggerCondition()) {
                    Triggered?.Invoke();
                }
            }
        }
    }
}
