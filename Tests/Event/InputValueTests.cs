using NSubstitute;

using NUnit.Framework;

using Tests.Helper;

using UnityScriptLab.Input.Event;

namespace Tests {
    namespace Event {
        public class InputValueTests {
            [SetUp]
            public void Reset() {
                InputEvent.ResetBindings();
            }

            [Test]
            public void AxisTest() {
                InputValueSpy spy = new InputValueSpy(ValueOf.Axis("Horizontal"));

                spy.SimulateInput(i => i.GetAxis("Horizontal").Returns(2.0f));
                spy.AssertWasUpdatedTo(2.0f);

                spy.WaitFrame();
                spy.AssertNothingHappened();
            }

            [Test]
            public void RawAxisTest() {
                InputValueSpy spy = new InputValueSpy(ValueOf.RawAxis("Horizontal"));

                spy.SimulateInput(i => i.GetAxisRaw("Horizontal").Returns(2.0f));
                spy.AssertWasUpdatedTo(2.0f);

                spy.WaitFrame();
                spy.AssertNothingHappened();
            }

            [Test]
            public void WithoutSignTest() {
                InputValueSpy spy = new InputValueSpy(new InputValue("value", _ => -2.0f).WithoutSign);

                spy.WaitFrame();
                spy.AssertWasUpdatedTo(2.0f);
            }
        }
    }
}
