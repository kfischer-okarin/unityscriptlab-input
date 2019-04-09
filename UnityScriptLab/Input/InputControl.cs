using System;

using UnityEngine;

namespace UnityScriptLab {
    namespace Input {
        public class InputControl {
            public event Action Triggered;
            public event Action Stopped;

            bool active;
            protected Func<bool> triggerCondition;
            protected Func<bool> stopCondition;

            public InputControl() {
                this.triggerCondition = () => false;
                this.stopCondition = () => true;
            }

            public void HandleInput() {
                if (!active && triggerCondition()) {
                    Triggered?.Invoke();
                    active = true;
                } else if (active && stopCondition()) {
                    Stopped?.Invoke();
                    active = false;
                }
            }

            public InputControl And(InputControl other) {
                var currentTriggerCondition = this.triggerCondition;
                var currentStopCondition = this.stopCondition;
                this.triggerCondition = () => currentTriggerCondition() && other.triggerCondition();
                this.stopCondition = () => currentStopCondition() || other.stopCondition();
                return this;
            }
        }
    }
}
