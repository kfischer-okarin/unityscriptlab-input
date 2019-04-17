using System;

using UnityEngine;

namespace UnityScriptLab {
    namespace Input {
        namespace Event {
            public static class ValueOf {
                /// <summary>
                /// InputValue: Axis.
                /// </summary>
                public static InputValue Axis(string name) => new InputValue($"Axis-{name}",
                    input => input.GetAxis(name));

                /// <summary>
                /// InputValue: Raw Axis.
                /// </summary>
                public static InputValue RawAxis(string name) => new InputValue($"RawAxis-{name}",
                    input => input.GetAxisRaw(name));

            }
        }
    }
}
