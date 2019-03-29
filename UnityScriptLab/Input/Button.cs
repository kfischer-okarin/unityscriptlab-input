using System;

namespace UnityScriptLab {
    namespace Input {
        public class Button : InputControl {
            public static Button ButtonPressed(string buttonName) {
                return new Button(buttonName, State.Pressed);
            }

            enum State {
                Pressed,
            }
            string buttonName;
            State buttonState;
            private Button(string buttonName, State buttonState) {
                this.buttonName = buttonName;
                this.buttonState = buttonState;
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
