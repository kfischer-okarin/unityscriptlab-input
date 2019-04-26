using System;

using UnityEngine;

namespace UnityScriptLab {
    namespace Input {
        namespace Event {
            public static class Key {
                /// <summary>
                /// InputTrigger: Key was pressed.
                /// </summary>
                public static Trigger Pressed(KeyCode key) => new Trigger($"KeyPressed-{key}",
                    input => input.GetKeyDown(key));

                /// <summary>
                /// InputTrigger: Key was released.
                /// </summary>
                public static Trigger Released(KeyCode key) => new Trigger($"KeyReleased-{key}",
                    input => input.GetKeyUp(key));

                /// <summary>
                /// InputTrigger: Key is being held down.
                /// (Triggers every frame the key is held down.)
                /// </summary>
                public static Trigger Held(KeyCode key) => new Trigger($"KeyHeld-{key}",
                    input => input.GetKey(key));

            }
        }
    }
}
