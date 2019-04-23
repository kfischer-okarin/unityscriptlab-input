using System;

using NSubstitute;

using NUnit.Framework;

using Tests.Helper;

using UnityEngine;
using UnityEngine.TestTools;

using UnityScriptLab.Input;
using UnityScriptLab.Input.Event;

namespace Tests {
    namespace Event {
        public class DerivedInputTriggerTests {
            [SetUp]
            public void Reset() {
                InputEvent.ResetBindings();
            }

            [Test]
            public void ValueOverTest() {
                InputValueStub value = new InputValueStub();
                InputTriggerSpy spy = new InputTriggerSpy(value.IsOver(2.0f));

                spy.WaitFrame();
                spy.AssertNothingHappened();

                value.Update(2.5f);
                spy.WaitFrame();
                spy.AssertWasTriggered();

                spy.WaitFrame();
                spy.AssertWasTriggered();

                value.Update(1.5f);
                spy.WaitFrame();
                spy.AssertWasStopped();
            }

            [Test]
            public void ValueBelowTest() {
                InputValueStub value = new InputValueStub();
                InputTriggerSpy spy = new InputTriggerSpy(value.IsBelow(2.0f));
                value.Update(3.0f);

                spy.WaitFrame();
                spy.AssertNothingHappened();

                value.Update(1.5f);
                spy.WaitFrame();
                spy.AssertWasTriggered();

                spy.WaitFrame();
                spy.AssertWasTriggered();

                value.Update(2.5f);
                spy.WaitFrame();
                spy.AssertWasStopped();
            }

            [Test]
            public void ValueSurpassedTest() {
                InputValueStub value = new InputValueStub();
                InputTriggerSpy spy = new InputTriggerSpy(value.Surpassed(2.0f));

                spy.WaitFrame();
                spy.AssertNothingHappened();

                value.Update(2.5f);
                spy.WaitFrame();
                spy.AssertWasTriggered();

                spy.WaitFrame();
                spy.AssertWasStopped();

                value.Update(1.5f);
                spy.WaitFrame();
                spy.AssertNothingHappened();
            }

            [Test]
            public void ValueFellBelowTest() {
                InputValueStub value = new InputValueStub();
                InputTriggerSpy spy = new InputTriggerSpy(value.FellBelow(2.0f));
                value.Update(2.5f);

                spy.WaitFrame();
                spy.AssertNothingHappened();

                value.Update(1.5f);
                spy.WaitFrame();
                spy.AssertWasTriggered();

                spy.WaitFrame();
                spy.AssertWasStopped();

                value.Update(2.5f);
                spy.WaitFrame();
                spy.AssertNothingHappened();
            }
        }
    }
}
