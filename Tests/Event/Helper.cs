using System;

using NSubstitute;

using NUnit.Framework;

using UnityScriptLab.Input;
using UnityScriptLab.Input.Event;

namespace Tests {
  namespace Helper {
    public class InputTriggerSpy {
      InputTrigger trigger;
      bool triggered;
      bool stopped;
      InputSystem input;

      public InputTriggerSpy(InputTrigger trigger) {
        this.trigger = trigger;
        input = Substitute.For<InputSystem>();
        trigger.Triggered += () => triggered = true;
        trigger.Stopped += () => stopped = true;
        trigger.SetInputSystem(input);
      }

      public void SimulateInput(Action<InputSystem> action) {
        triggered = false;
        stopped = false;
        action?.Invoke(input);
        trigger.HandleInput();
      }

      public void WaitFrame() {
        SimulateInput(null);
      }

      public void AssertWasTriggered() {
        Assert.That(triggered, Is.True, "But wasn't triggered");
        Assert.That(stopped, Is.False, "But was stopped");
      }

      public void AssertWasStopped() {
        Assert.That(triggered, Is.False, "But was triggered");
        Assert.That(stopped, Is.True, "But wasn't stopped");
      }

      public void AssertNothingHappened() {
        Assert.That(triggered, Is.False, "But was triggered");
        Assert.That(stopped, Is.False, "But was stopped");
      }
    }
  }
}
