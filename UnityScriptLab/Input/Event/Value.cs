using System;
using System.Collections.Generic;

using UnityEngine;

namespace UnityScriptLab {
    namespace Input {
        namespace Event {
            /// <summary>
            /// Value emitted by the input system, to which handlers can be subscribed.
            /// </summary>
            public abstract class Value<T> : InputEvent {
                /// <param name="name">Unique name of the event</param>
                public Value(string name) : base(name) { }

                event Action<T> updated;

                /// <summary>
                /// Triggered every frame the value changed.
                /// </summary>
                public event Action<T> Updated {
                    add {
                        Bind<Value<T>>(v => v.updated += value);
                    }
                    remove {
                        Unbind<Value<T>>(v => v.updated -= value);
                    }
                }

                /// <summary>
                /// Get the current value of the input.
                /// </summary>
                public abstract T GetValue(InputSystem input);

                T value;
                T newValue;
                bool newValueUpdated = false;
                T NewValue {
                    get {
                        if (!newValueUpdated) {
                            newValue = GetValue(this.Input);
                        }
                        return newValue;
                    }
                }

                /// <summary>
                /// Broadcast the specified value to all subscribed listeners.
                /// </summary>
                protected void Broadcast(T value) {
                    updated?.Invoke(value);
                }

                /// <summary>
                /// Should return `true` if the new value should be broadcasted to the listeners.
                /// </summary>
                /// <param name="value">Current value</param>
                /// <param name="lastValue">Value of the last frame</param>
                /// <returns></returns>
                public virtual bool ShouldBroadcast(T value, T lastValue) => true;

                public override void HandleInput() {
                    newValueUpdated = false;
                    if (ShouldBroadcast(NewValue, value)) {
                        Broadcast(NewValue);
                    }
                    value = NewValue;
                }
            }
        }
    }
}
