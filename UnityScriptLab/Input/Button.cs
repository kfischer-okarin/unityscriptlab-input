using System;

using UnityEngine;

namespace UnityScriptLab {
    namespace Input {
        public class Button : InputControl {
            enum State {
                Pressed,
                Released,
            }
            string buttonName;
            State buttonState;
            public Button(string buttonName) {
                this.buttonName = buttonName;
                this.buttonState = State.Pressed;
            }

            public event Action Triggered;

            public void Update() {
                switch (buttonState) {
                    case State.Pressed:
                        if (UnityEngine.Input.GetButtonDown(buttonName)) {
                            Triggered?.Invoke();
                        }
                        break;
                }
            }
        }
    }
}
