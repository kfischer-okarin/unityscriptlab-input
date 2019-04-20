using NSubstitute;

using NUnit.Framework;

using Tests.Helper;

using UnityEngine;

using UnityScriptLab.Input;
using UnityScriptLab.Input.Event;

namespace Tests {
    namespace Event {
        public class InputTriggerTests {
            [SetUp]
            public void Reset() {
                InputEvent.ResetBindings();
            }

            [Test]
            public void KeyPressedTest() {
                InputTriggerSpy spy = new InputTriggerSpy(Key.Pressed(KeyCode.Space));

                spy.SimulateInput(i => i.GetKeyDown(KeyCode.Space).Returns(true));
                spy.AssertWasTriggered();

                spy.SimulateInput(i => i.GetKeyDown(KeyCode.Space).Returns(false));
                spy.AssertWasStopped();
            }

            [Test]
            public void KeyReleasedTest() {
                InputTriggerSpy spy = new InputTriggerSpy(Key.Released(KeyCode.Space));

                spy.SimulateInput(i => i.GetKeyUp(KeyCode.Space).Returns(true));
                spy.AssertWasTriggered();

                spy.SimulateInput(i => i.GetKeyUp(KeyCode.Space).Returns(false));
                spy.AssertWasStopped();
            }

            [Test]
            public void KeyHeldTest() {
                InputTriggerSpy spy = new InputTriggerSpy(Key.Held(KeyCode.Space));

                spy.SimulateInput(i => i.GetKey(KeyCode.Space).Returns(true));
                spy.AssertWasTriggered();

                spy.WaitFrame();
                spy.AssertWasTriggered();

                spy.SimulateInput(i => i.GetKey(KeyCode.Space).Returns(false));
                spy.AssertWasStopped();
            }

            [Test]
            public void AndTest() {
                bool aTriggered = false;
                bool bTriggered = false;
                InputTrigger triggerA = new InputTrigger("A", _ => aTriggered);
                InputTrigger triggerB = new InputTrigger("B", _ => bTriggered);
                InputTrigger combined = triggerA.And(triggerB);

                Assert.That(combined.ToString(), Is.EqualTo("A+B"));
                InputTriggerSpy spy = new InputTriggerSpy(combined);

                aTriggered = true;
                bTriggered = false;
                spy.WaitFrame();
                spy.AssertNothingHappened();

                aTriggered = false;
                bTriggered = true;
                spy.WaitFrame();
                spy.AssertNothingHappened();

                aTriggered = true;
                bTriggered = true;
                spy.WaitFrame();
                spy.AssertWasTriggered();

                aTriggered = false;
                bTriggered = true;
                spy.WaitFrame();
                spy.AssertWasStopped();

                aTriggered = false;
                bTriggered = false;
                spy.WaitFrame();
                spy.AssertNothingHappened();
            }
        }
    }
}
