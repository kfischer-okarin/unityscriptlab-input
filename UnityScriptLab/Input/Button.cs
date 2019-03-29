using System;

namespace UnityScriptLab {
    namespace Input {
        public class Button : InputControl {
            string buttonName;
            public Button(string buttonName) {
                this.buttonName = buttonName;
            }

            public event Action Triggered;

            public void Update() {
                if (UnityEngine.Input.GetButtonDown(buttonName)) {
                    Triggered?.Invoke();
                }
            }
        }
    }
}
