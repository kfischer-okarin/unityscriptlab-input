using System;

using NSubstitute;

using NUnit.Framework;

using UnityEngine;
using UnityEngine.TestTools;

using UnityScriptLab.Input;
using UnityScriptLab.Input.Event;

namespace Tests {
    namespace Event {
        public class InputValueTests {
            float value;
            InputSystem input;
            InputValue val;

            void Prepare(InputValue valueToPrepare) {
                InputEvent.ResetBindings();
                val = valueToPrepare;
                val.Updated += v => value = v;
                input = Substitute.For<InputSystem>();
                val.SetInputSystem(input);
            }

            void SimulateInput(Action<InputSystem> action) {
                value = 0;
                action?.Invoke(input);
                val.HandleInput();
            }

            void WaitFrame() {
                SimulateInput(null);
            }

            void AssertOnlyNotifiesWhenChanging(Func<InputSystem, float>  valueGetter) {
                SimulateInput(i => valueGetter(i).Returns(2.0f));
                Assert.That(value, Is.EqualTo(2.0f));

                SimulateInput(i => valueGetter(i).Returns(2.0f));
                Assert.That(value, Is.EqualTo(0.0f));

                SimulateInput(i => valueGetter(i).Returns(3.0f));
                Assert.That(value, Is.EqualTo(3.0f));
            }

            [Test]
            public void AxisTest() {
                Prepare(ValueOf.Axis("Horizontal"));

                AssertOnlyNotifiesWhenChanging(i => i.GetAxis("Horizontal"));
            }

            [Test]
            public void RawAxisTest() {
                Prepare(ValueOf.RawAxis("Horizontal"));

                AssertOnlyNotifiesWhenChanging(i => i.GetAxisRaw("Horizontal"));
            }
        }
    }
}
