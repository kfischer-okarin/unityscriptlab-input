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
                float newValue = 0.0f;
                bool newValueUpdated = false;
                float NewValue {
                    get {
                        if (!newValueUpdated) {
                            newValue = getValue(this.Input);
                        }
                        return newValue;
                    }
                }
                bool HasNewValue { get { return !Mathf.Approximately(value, NewValue); } }

                Func<InputSystem, float> getValue;

                /// <param name="name">Unique name of the event</param>
                /// <param name="triggerCondition">Condition for the event triggering.</param>
                /// <param name="stopCondition">Condition for the event stopping.</param>
                public InputValue(string name, Func<InputSystem, float> getValue) : base(name) {
                    this.getValue = getValue;
                }

                protected void BroadcastUpdate(float value) {
                    updated?.Invoke(value);
                }

                public override void HandleInput() {
                    newValueUpdated = false;
                    if (HasNewValue) {
                        value = NewValue;
                        BroadcastUpdate(value);
                    }
                }

                public InputValue WithoutSign {
                    get {
                        return new InputValue($"{name}-WithoutSign", input => Mathf.Abs(getValue(input)));
                    }
                }

                private Func<InputSystem, bool> ValueCondition(Func<float, bool> afterChange, Func<float, bool> beforeChange) {
                    return _ => {
                        if (!beforeChange(value)) {
                            return false;
                        }
                        this.HandleInput();
                        return afterChange(value);
                    };
                }

                private Func<InputSystem, bool> ValueCondition(Func<float, bool> afterChange) {
                    return ValueCondition(afterChange, _ => true);
                }

                public InputTrigger IsOver(float threshold) {
                    return new InputTrigger($"{name}-IsOver-{threshold}", ValueCondition(value => value > threshold));
                }

                public InputTrigger IsBelow(float threshold) {
                    return new InputTrigger($"{name}-IsBelow-{threshold}", ValueCondition(value => value < threshold));
                }

                public InputTrigger Surpassed(float threshold) {
                    return new InputTrigger($"{name}-Surpassed-{threshold}",
                        ValueCondition(value => value > threshold, value => value <= threshold));
                }

                public InputTrigger FellBelow(float threshold) {
                    return new InputTrigger($"{name}-FellBelow-{threshold}",
                        ValueCondition(value => value < threshold, value => value >= threshold));
                }
            }
        }
    }
}
