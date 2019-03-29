using System;

using UnityEngine;

namespace UnityScriptLab {
    namespace Input {
        public class Button : InputControl {
            enum State {
                Pressed,
                Released,
            }
            KeyCode key;
            State buttonState;
            public Button(KeyCode key) {
                this.key = key;
                this.buttonState = State.Pressed;
            }

            public event Action Triggered;

            public void Update() {
                switch (buttonState) {
                    case State.Pressed:
                        if (UnityEngine.Input.GetKeyDown(key)) {
                            Triggered?.Invoke();
                        }
                        break;
                }
            }
        }
    }
}
