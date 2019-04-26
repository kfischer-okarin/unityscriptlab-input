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
        public class DerivedInputValueTests {
            [SetUp]
            public void Reset() {
                InputEvent.ResetBindings();
            }

            [Test]
            public void TriggerAsValueTest() {
                TriggerStub trigger = new TriggerStub();
                ValueSpy<float> spy = new ValueSpy<float>(trigger.AsValue());

                spy.WaitFrame();
                spy.AssertNothingHappened();

                trigger.Update(true);
                spy.WaitFrame();
                spy.AssertWasUpdatedTo(1);

                spy.WaitFrame();
                spy.AssertNothingHappened();

                trigger.Update(false);
                spy.WaitFrame();
                spy.AssertWasUpdatedTo(0);
            }

        }
    }
}
