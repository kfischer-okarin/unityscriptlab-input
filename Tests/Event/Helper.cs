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

    public class InputValueSpy {
      InputValue val;
      float newValue;
      bool updated;
      InputSystem input;

      public InputValueSpy(InputValue val) {
        this.val = val;
        input = Substitute.For<InputSystem>();
        val.Updated += v => {
          updated = true;
          newValue = v;
        };
        val.SetInputSystem(input);
      }

      public void SimulateInput(Action<InputSystem> action) {
        updated = false;
        action?.Invoke(input);
        val.HandleInput();
      }

      public void WaitFrame() {
        SimulateInput(null);
      }

      public void AssertWasUpdatedTo(float value) {
        Assert.That(updated, Is.True, "But wasn't updated");
        Assert.That(newValue, Is.EqualTo(value), $"But was updated to {newValue}");
      }

      public void AssertNothingHappened() {
        Assert.That(updated, Is.False, "But was updated");
      }
    }
  }
}
