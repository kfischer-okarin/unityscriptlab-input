using System;

using UnityEngine;

namespace UnityScriptLab {
    namespace Input {
        namespace Event {

            public static class Key {
                /// <summary>
                /// InputEvent: Key was pressed.
                /// </summary>
                public static InputEvent Pressed(KeyCode key) => new InputEvent($"KeyPressed-{key}",
                    input => input.GetKeyDown(key));

                /// <summary>
                /// InputEvent: Key was released.
                /// </summary>
                public static InputEvent Released(KeyCode key) => new InputEvent($"KeyReleased-{key}",
                    input => input.GetKeyUp(key));

                /// <summary>
                /// InputEvent: Key is being held down.
                /// (Triggers every frame the key is held down.)
                /// </summary>
                public static InputEvent Held(KeyCode key) => new InputEvent($"KeyHeld-{key}",
                    input => input.GetKey(key),
                    input => input.GetKeyUp(key));
            }
        }
    }
}
