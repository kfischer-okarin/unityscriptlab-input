using System;

using UnityEngine;

namespace UnityScriptLab {
    namespace Input {
        namespace Event {

            public static class Key {
                public static InputEvent Pressed(KeyCode key) {
                    return new InputEvent($"KeyPressed-{key}", input => input.GetKeyDown(key));
                }
                public static InputEvent Released(KeyCode key) {
                    return new InputEvent($"KeyReleased-{key}", input => input.GetKeyUp(key));
                }
                public static InputEvent Held(KeyCode key) {
                    return new InputEvent($"KeyHeld-{key}", input => input.GetKey(key), input => input.GetKeyUp(key));
                }
            }
        }
    }
}
