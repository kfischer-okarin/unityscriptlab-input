using System;
using System.Collections.Generic;

using UnityEngine;

namespace UnityScriptLab {
    namespace Input {
        namespace Event {
            /// <summary>
            /// Event triggered by the input system, to which handlers can be subscribed.
            /// </summary>
            public class InputTrigger : InputEvent {
                event Action triggered;
                event Action stopped;

                /// <summary>
                /// Triggered every frame the event is happening.
                /// </summary>
                public event Action Triggered {
                    add {
                        Bind<InputTrigger>(ev => ev.triggered += value);
                    }
                    remove {
                        Unbind<InputTrigger>(ev => ev.triggered -= value);
                    }
                }

                /// <summary>
                /// Triggered on the frame the event ends.
                /// </summary>
                public event Action Stopped {
                    add {
                        Bind<InputTrigger>(ev => ev.stopped += value);
                    }
                    remove {
                        Unbind<InputTrigger>(ev => ev.stopped -= value);
                    }
                }

                bool active;
                protected Func<InputSystem, bool> triggerCondition = _ => false;
                protected Func<InputSystem, bool> stopCondition;

                /// <param name="name">Unique name of the event</param>
                /// <param name="triggerCondition">Condition for the event triggering.</param>
                /// <param name="stopCondition">Condition for the event stopping.</param>
                public InputTrigger(string name, Func<InputSystem, bool> triggerCondition, Func<InputSystem, bool> stopCondition) : base(name) {
                    this.triggerCondition = triggerCondition;
                    this.stopCondition = stopCondition;
                }

                /// <param name="name">Unique name of the event</param>
                /// <param name="triggerCondition">Condition for the event triggering.</param>
                public InputTrigger(string name, Func<InputSystem, bool> triggerCondition) : this(name, triggerCondition, i => !triggerCondition(i)) { }


                /// <param name="name">Unique name of the event</param>
                public InputTrigger(string name) : this(name, _ => false) { }

                public override void HandleInput() {
                    if (active) {
                        if (stopCondition(this.Input)) {
                            active = false;
                            stopped?.Invoke();
                            return;
                        }
                        triggered?.Invoke();
                    } else if (triggerCondition(this.Input)) {
                        active = true;
                        triggered?.Invoke();
                    }
                }

                /// <summary>
                /// Returns a combined trigger occurring when both of the events are triggering at the same time.
                /// </summary>
                public InputTrigger And(InputTrigger other) {
                    return new InputTrigger($"{this.name}+{other.name}",
                        input => this.triggerCondition(input) && other.triggerCondition(input),
                        input => this.stopCondition(input) || other.stopCondition(input));
                }
            }
        }
    }
}
