﻿using System;

using NSubstitute;

using NUnit.Framework;

using UnityEngine;
using UnityEngine.TestTools;

using UnityScriptLab.Input;
using UnityScriptLab.Input.Event;

namespace Tests {
    namespace Event {
        public class InputTriggerTests {
            bool triggered;
            bool stopped;
            InputSystem input;
            InputTrigger ev;

            [SetUp]
            public void Reset() {
                InputEvent.ResetBindings();
                input = Substitute.For<InputSystem>();
            }

            void Prepare(InputTrigger eventToPrepare) {
                ev = eventToPrepare;
                ev.Triggered += () => triggered = true;
                ev.Stopped += () => stopped = true;
                ev.SetInputSystem(input);
            }

            void SimulateInput(Action<InputSystem> action) {
                triggered = false;
                stopped = false;
                action?.Invoke(input);
                ev.HandleInput();
            }

            void WaitFrame() {
                SimulateInput(null);
            }

            void AssertEventWasTriggered() {
                Assert.That(triggered, Is.True);
                Assert.That(stopped, Is.False);
            }

            void AssertEventWasStopped() {
                Assert.That(triggered, Is.False);
                Assert.That(stopped, Is.True);
            }

            void AssertNothingHappened() {
                Assert.That(triggered, Is.False);
                Assert.That(stopped, Is.False);
            }

            [Test]
            public void KeyPressedTest() {
                Prepare(Key.Pressed(KeyCode.Space));

                SimulateInput(i => i.GetKeyDown(KeyCode.Space).Returns(true));
                AssertEventWasTriggered();

                SimulateInput(i => i.GetKeyDown(KeyCode.Space).Returns(false));
                AssertEventWasStopped();
            }

            [Test]
            public void KeyReleasedTest() {
                Prepare(Key.Released(KeyCode.Space));

                SimulateInput(i => i.GetKeyUp(KeyCode.Space).Returns(true));
                AssertEventWasTriggered();

                SimulateInput(i => i.GetKeyUp(KeyCode.Space).Returns(false));
                AssertEventWasStopped();
            }

            [Test]
            public void KeyHeldTest() {
                Prepare(Key.Held(KeyCode.Space));

                SimulateInput(i => i.GetKey(KeyCode.Space).Returns(true));
                AssertEventWasTriggered();

                WaitFrame();
                AssertEventWasTriggered();

                SimulateInput(i => i.GetKey(KeyCode.Space).Returns(false));
                AssertEventWasStopped();
            }

            [Test]
            public void AndTest() {
                bool aTriggered = false;
                bool bTriggered = false;
                InputTrigger triggerA = new InputTrigger("A", _ => aTriggered);
                InputTrigger triggerB = new InputTrigger("B", _ => bTriggered);
                InputTrigger combined = triggerA.And(triggerB);

                Assert.That(combined.ToString(), Is.EqualTo("A+B"));
                Prepare(combined);

                aTriggered = true;
                bTriggered = false;
                WaitFrame();
                AssertNothingHappened();

                aTriggered = false;
                bTriggered = true;
                WaitFrame();
                AssertNothingHappened();

                aTriggered = true;
                bTriggered = true;
                WaitFrame();
                AssertEventWasTriggered();

                aTriggered = false;
                bTriggered = true;
                WaitFrame();
                AssertEventWasStopped();

                aTriggered = false;
                bTriggered = false;
                WaitFrame();
                AssertNothingHappened();
            }

            [Test]
            public void ValueOverTest() {
                float returnedValue = 0;
                InputValue value = new InputValue("value", _ => returnedValue);
                Prepare(value.IsOver(2.0f));

                WaitFrame();
                AssertNothingHappened();

                returnedValue = 2.5f;
                WaitFrame();
                AssertEventWasTriggered();

                WaitFrame();
                AssertEventWasTriggered();

                returnedValue = 1.5f;
                WaitFrame();
                AssertEventWasStopped();
            }

            [Test]
            public void ValueBelowTest() {
                float returnedValue = 3.0f;
                InputValue value = new InputValue("value", _ => returnedValue);
                Prepare(value.IsBelow(2.0f));

                WaitFrame();
                AssertNothingHappened();

                returnedValue = 1.5f;
                WaitFrame();
                AssertEventWasTriggered();

                WaitFrame();
                AssertEventWasTriggered();

                returnedValue = 2.5f;
                WaitFrame();
                AssertEventWasStopped();
            }
        }
    }
}
