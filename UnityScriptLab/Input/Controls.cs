using UnityEngine;

namespace UnityScriptLab {
    namespace Input {
        public static class Controls {
            public static Button Button(KeyCode key) => new Button(key);
        }
    }
}
