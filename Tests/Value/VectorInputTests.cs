using NSubstitute;

using NUnit.Framework;

using Tests.Helper;

using UnityEngine;

using UnityScriptLab.Input;
using UnityScriptLab.Input.Value;

namespace Tests.Value {
  public class VectorInputTests {
    [SetUp]
    public void Reset() {
      InputEvent.ResetBindings();
    }

    [Test]
    public void VectorFromScalarsTest() {
      ScalarStub x = new ScalarStub("x");
      ScalarStub y = new ScalarStub("y");
      ValueSpy<Vector2> spy = new ValueSpy<Vector2>(new Vector2Input("something", x, y));

      spy.WaitFrame();
      spy.AssertNothingHappened();

      x.Update(0.5f);
      spy.WaitFrame();
      spy.AssertWasUpdatedTo(new Vector2(0.5f, 0));

      spy.WaitFrame();
      spy.AssertNothingHappened();

      y.Update(-1);
      spy.WaitFrame();
      spy.AssertWasUpdatedTo(new Vector2(0.5f, -1));
    }
  }
}
