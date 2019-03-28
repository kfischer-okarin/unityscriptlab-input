using System.Collections.Generic;

using UnityEngine;

namespace UnityScriptLab {
    namespace Input {
        /// <summary>
        /// Input Action.
        /// </summary>
        public class InputAction {

            static Dictionary<string, InputAction> actions = new Dictionary<string, InputAction> ();

            string name;

            public static InputAction Get (string name) {
                if (!actions.ContainsKey (name)) {
                    actions[name] = new InputAction (name);
                }
                return actions[name];
            }

            private InputAction (string name) {
                this.name = name;
            }

        }
    }
}
