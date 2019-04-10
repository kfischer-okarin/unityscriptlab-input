using System;
using System.Collections.Generic;

using UnityEngine;

namespace UnityScriptLab {
    namespace Input {
        namespace Event {
            public class InputEvent {
                static Dictionary<string, InputEvent> boundEvents = new Dictionary<string, InputEvent>();

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
                        triggered?.Invoke();
                        active = true;
                    } else if (active && stopCondition()) {
                        stopped?.Invoke();
                        active = false;
                    }
                }

                public InputEvent And(InputEvent other) {
                    return new InputEvent($"{this.name}+{other.name}",
                        () => this.triggerCondition() && other.triggerCondition(),
                        () => this.stopCondition() || other.stopCondition());
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
