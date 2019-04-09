using UnityEngine;

namespace UnityScriptLab {
    namespace Input {
        public static class Controls {
            public static Key Key(KeyCode key) => new Key(key).Hold;
        }
    }
}
