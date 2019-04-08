using System;

using UnityEngine;

namespace UnityScriptLab {
    namespace Input {
        public class Button : InputControl {
            KeyCode key;
            Func<bool> TriggerCondition;
            Func<bool> StopCondition;
            bool active;

            public Button(KeyCode key) {
                this.key = key;
                this.TriggerCondition = () => false;
                this.StopCondition = () => false;
                this.active = false;
            }

            public Button Press {
                get {
                    this.TriggerCondition = () => UnityEngine.Input.GetKeyDown(key);
                    this.StopCondition = () => true;
                    return this;
                }
            }

            public Button Release {
                get {
                    this.TriggerCondition = () => UnityEngine.Input.GetKeyUp(key);
                    this.StopCondition = () => true;
                    return this;
                }
            }

            public Button Hold {
                get {
                    this.TriggerCondition = () => UnityEngine.Input.GetKey(key);
                    this.StopCondition = () => UnityEngine.Input.GetKeyUp(key);
                    return this;
                }
            }

            public Button And(Button other) {
                var currentTriggerCondition = this.TriggerCondition;
                var currentStopCondition = this.StopCondition;
                this.TriggerCondition = () => currentTriggerCondition() && other.TriggerCondition();
                this.StopCondition = () => currentStopCondition() && other.StopCondition();
                return this;
            }

            public event Action Triggered;
            public event Action Stopped;

            public void HandleInput() {
                if (!active && TriggerCondition()) {
                    Triggered?.Invoke();
                    active = true;
                } else if (active && StopCondition()) {
                    Stopped?.Invoke();
                    active = false;
                }
            }
        }
    }
}
