using System;
using System.Collections.Generic;

using UnityEngine;

namespace UnityScriptLab {
    namespace Input {
        namespace Event {
            /// <summary>
            /// Event triggered by the input system, to which handlers can be subscribed.
            /// </summary>
            public abstract class InputEvent {
                static Dictionary<string, InputEvent> boundEvents = new Dictionary<string, InputEvent>();

                /// <summary>
                /// All events that are currently bound to a handler.
                /// </summary>
                public static IReadOnlyCollection<InputEvent> BoundEvents {
                    get {
                        return boundEvents.Values;
                    }
                }

                public static void ResetBindings() {
                    boundEvents.Clear();
                }

                int bindingCount = 0;
                protected string name;
                protected InputSystem input = new UnityInputSystem();

                /// <param name="name">Unique name of the event</param>
                protected InputEvent(string name) {
                    this.name = name;
                }

                /// <summary>
                /// Set the InputSystem which is queried. Use to set stub input.
                /// </summary>
                public void SetInputSystem(InputSystem input) {
                    this.input = input;
                }

                public abstract void HandleInput();

                public override string ToString() => name;

                protected void Bind<T>(Action<T> doBind) where T : InputEvent {
                    if (!boundEvents.ContainsKey(name)) {
                        boundEvents[name] = this;
                    }
                    InputEvent target = boundEvents[name];
                    doBind((T) target);
                    target.bindingCount += 1;
                }

                protected void Unbind<T>(Action<T> doUnbind) where T : InputEvent  {
                    if (boundEvents.ContainsKey(name)) {
                        InputEvent target = boundEvents[name];
                        doUnbind((T) target);
                        target.bindingCount -= 1;
                        if (target.bindingCount == 0) {
                            boundEvents.Remove(name);
                        }
                    }
                }
            }
        }
    }
}
