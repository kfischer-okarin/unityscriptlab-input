using System;
using System.Collections.Generic;

namespace UnityScriptLab {
    namespace Input {
        using Value;

        /// <summary>
        /// Input Action.
        /// </summary>
        public class InputAction {

            static Dictionary<string, InputAction> actions = new Dictionary<string, InputAction>();

            public static InputAction Get(string name) {
                if (!actions.ContainsKey(name)) {
                    actions[name] = new InputAction(name);
                }
                return actions[name];
            }

            public event Action<InputEvent> Triggered;

            string name;

            List<InputEvent> bindings;

            private InputAction(string name) {
                this.name = name;
                this.bindings = new List<InputEvent>();
            }

            public void Bind(TriggerInput control) {
                bindings.Add(control);
                control.Updated += value => {
                    if (value) Triggered?.Invoke(control);
                };
            }

        }
    }
}
