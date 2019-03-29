using UnityEngine;

namespace UnityScriptLab {
    namespace Input {
        public static class Controls {
            public static Button Button(string buttonName) => new Button(buttonName);
        }
    }
}
