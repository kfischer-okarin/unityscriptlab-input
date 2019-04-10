using System;
using System.Collections.Generic;

using UnityEngine;

namespace UnityScriptLab {
    namespace Input {
        namespace Event {
            public class InputEvent {
                public event Action Triggered;
                public event Action Stopped;

                string name;
                bool active;
                Func<bool> triggerCondition;
                Func<bool> stopCondition;

                public InputEvent(string name, Func<bool> triggerCondition, Func<bool> stopCondition) {
                    this.name = name;
                    this.triggerCondition = triggerCondition;
                    this.stopCondition = stopCondition;
                }
                public InputEvent(string name, Func<bool> triggerCondition) : this(name, triggerCondition, () => true) { }

                public void HandleInput() {
                    if (!active && triggerCondition()) {
                        Triggered?.Invoke();
                        active = true;
                    } else if (active && stopCondition()) {
                        Stopped?.Invoke();
                        active = false;
                    }
                }

                public InputEvent And(InputEvent other) {
                    return new InputEvent($"{this.name}+{other.name}",
                        () => this.triggerCondition() && other.triggerCondition(),
                        () => this.stopCondition() || other.stopCondition());
                }

                public override string ToString() => name;
            }
        }
    }
}
