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

            public Button Hold {
                get {
                    this.TriggerCondition = () => UnityEngine.Input.GetKey(key);
                    return this;
                }
            }

            public Button And(Button other) {
                var currentCondition = this.TriggerCondition;
                this.TriggerCondition = () => currentCondition() && other.TriggerCondition();
                return this;
            }

            public event Action Triggered;

            public void HandleInput() {
                if (TriggerCondition()) {
                    Triggered?.Invoke();
                }
            }
        }
    }
}
