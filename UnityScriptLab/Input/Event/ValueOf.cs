using System;

using UnityEngine;

namespace UnityScriptLab {
    namespace Input {
        namespace Event {
            public static class ValueOf {
                /// <summary>
                /// InputValue: Axis.
                /// </summary>
                public static ScalarInput Axis(string name) => new ScalarInput($"Axis-{name}",
                    input => input.GetAxis(name));

                /// <summary>
                /// InputValue: Raw Axis.
                /// </summary>
                public static ScalarInput RawAxis(string name) => new ScalarInput($"RawAxis-{name}",
                    input => input.GetAxisRaw(name));

                // public static InputValue TwoTriggerAxis(InputTrigger positive, InputTrigger negative) {

                // }

            }
        }
    }
}
