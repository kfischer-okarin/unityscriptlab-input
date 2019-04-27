using System;

using UnityEngine;

namespace UnityScriptLab {
    namespace Input {
        namespace Event {
            public static class Key {
                /// <summary>
                /// InputTrigger: Key was pressed.
                /// </summary>
                public static TriggerInput Pressed(KeyCode key) => new TriggerInput($"KeyPressed-{key}",
                    input => input.GetKeyDown(key));

                /// <summary>
                /// InputTrigger: Key was released.
                /// </summary>
                public static TriggerInput Released(KeyCode key) => new TriggerInput($"KeyReleased-{key}",
                    input => input.GetKeyUp(key));

                /// <summary>
                /// InputTrigger: Key is being held down.
                /// (Triggers every frame the key is held down.)
                /// </summary>
                public static TriggerInput Held(KeyCode key) => new TriggerInput($"KeyHeld-{key}",
                    input => input.GetKey(key));

            }
        }
    }
}
