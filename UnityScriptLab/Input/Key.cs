using System;

using UnityEngine;

namespace UnityScriptLab {
    namespace Input {
        public class Key {
            public static InputEvent Pressed(KeyCode key) {
                return new InputEvent($"KeyPressed-{key}", () => UnityEngine.Input.GetKeyDown(key));
            }
            public static InputEvent Released(KeyCode key) {
                return new InputEvent($"KeyReleased-{key}", () => UnityEngine.Input.GetKeyUp(key));
            }
            public static InputEvent Held(KeyCode key) {
                return new InputEvent($"KeyHeld-{key}",
                    () => UnityEngine.Input.GetKey(key),
                    () => UnityEngine.Input.GetKeyUp(key));
            }
        }
    }
}
