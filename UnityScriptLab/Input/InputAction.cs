using System;
using System.Collections.Generic;

using UnityEngine;

namespace UnityScriptLab {
    namespace Input {
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

            public void Bind(InputEvent control) {
                bindings.Add(control);
                control.Triggered += () => Triggered?.Invoke(control);
            }

        }
    }
}
