using System;
using System.Collections.Generic;

using UnityEngine;

namespace UnityScriptLab {
    namespace Input {
        namespace Event {
            public class InputEvent {
                static Dictionary<string, InputEvent> boundEvents = new Dictionary<string, InputEvent>();

                public static IReadOnlyCollection<InputEvent> BoundEvents {
                    get {
                        return boundEvents.Values;
                    }
                }

                public static void ResetBindings() {
                    boundEvents.Clear();
                }

                int bindingCount = 0;
                protected event Action triggered;
                protected event Action stopped;

                public event Action Triggered {
                    add {
                        Bind(ev => ev.triggered += value);
                    }
                    remove {
                        Unbind(ev => ev.triggered -= value);
                    }
                }
                public event Action Stopped {
                    add {
                        Bind(ev => ev.stopped += value);
                    }
                    remove {
                        Unbind(ev => ev.stopped -= value);
                    }
                }

                string name;
                bool active;
                Func<InputSystem, bool> triggerCondition;
                Func<InputSystem, bool> stopCondition;
                InputSystem input = new UnityInputSystem();

                public InputEvent(string name, Func<InputSystem, bool> triggerCondition, Func<InputSystem, bool> stopCondition) {
                    this.name = name;
                    this.triggerCondition = triggerCondition;
                    this.stopCondition = stopCondition;
                }
                public InputEvent(string name, Func<InputSystem, bool> triggerCondition) : this(name, triggerCondition, input => true) { }

                public void SetInputSystem(InputSystem input) {
                    this.input = input;
                }

                public void HandleInput() {
                    if (!active && triggerCondition(input)) {
                        triggered?.Invoke();
                        active = true;
                    } else if (active && stopCondition(input)) {
                        stopped?.Invoke();
                        active = false;
                    }
                }

                public InputEvent And(InputEvent other) {
                    return new InputEvent($"{this.name}+{other.name}",
                        input => this.triggerCondition(input) && other.triggerCondition(input),
                        input => this.stopCondition(input) || other.stopCondition(input));
                }

                public override string ToString() => name;

                void Bind(Action<InputEvent> doBind) {
                    if (!boundEvents.ContainsKey(name)) {
                        boundEvents[name] = this;
                    }
                    InputEvent target = boundEvents[name];
                    doBind(target);
                    target.bindingCount += 1;
                }

                void Unbind(Action<InputEvent> doUnbind) {
                    if (boundEvents.ContainsKey(name)) {
                        InputEvent target = boundEvents[name];
                        doUnbind(target);
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
