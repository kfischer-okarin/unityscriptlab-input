using System;

using UnityEngine;

namespace UnityScriptLab {
    namespace Input {
        namespace Event {

            public static class Key {
                /// <summary>
                /// InputTrigger: Key was pressed.
                /// </summary>
                public static InputTrigger Pressed(KeyCode key) => new InputTrigger($"KeyPressed-{key}",
                    input => input.GetKeyDown(key));

                /// <summary>
                /// InputTrigger: Key was released.
                /// </summary>
                public static InputTrigger Released(KeyCode key) => new InputTrigger($"KeyReleased-{key}",
                    input => input.GetKeyUp(key));

                /// <summary>
                /// InputTrigger: Key is being held down.
                /// (Triggers every frame the key is held down.)
                /// </summary>
                public static InputTrigger Held(KeyCode key) => new InputTrigger($"KeyHeld-{key}",
                    input => input.GetKey(key),
                    input => input.GetKeyUp(key));
            }
        }
    }
}
