using System;
using System.Collections.Generic;

using UnityEngine;

namespace UnityScriptLab {
    namespace Input {
        namespace Event {
            /// <summary>
            /// Value emitted by the input system, to which handlers can be subscribed.
            /// </summary>
            public class InputValue : InputEvent {
                event Action<float> updated;

                /// <summary>
                /// Triggered every frame the value changed.
                /// </summary>
                public event Action<float> Updated {
                    add {
                        Bind<InputValue>(ev => ev.updated += value);
                    }
                    remove {
                        Unbind<InputValue>(ev => ev.updated -= value);
                    }
                }

                float value = 0.0f;
                Func<InputSystem, float> getValue;

                /// <param name="name">Unique name of the event</param>
                /// <param name="triggerCondition">Condition for the event triggering.</param>
                /// <param name="stopCondition">Condition for the event stopping.</param>
                public InputValue(string name, Func<InputSystem, float> getValue) : base(name) {
                    this.getValue = getValue;
                }

                public override void HandleInput() {
                    float current = getValue(this.Input);
                    if (!Mathf.Approximately(value, current)) {
                        value = current;
                        updated?.Invoke(value);
                    }
                }
            }
        }
    }
}
