using UnityEngine;

namespace UnityScriptLab.Input {
  using Value;
  using Value.Provider;

  public static class Combine {
    public static ScalarInput TriggersToAxis(TriggerInput positive, TriggerInput negative) {
      return new ScalarInput($"Axis({positive}, {negative})", new TriggerAsAxis(positive, negative));
    }

    public static Vector2Input ScalarsToVector(ScalarInput x, ScalarInput y) {
      return new Vector2Input($"Vector({x}, {y})", new CombineScalars(x, y));
    }

    class TriggerAsAxis : Combinator<bool, bool, float> {
      public TriggerAsAxis(TriggerInput positive, TriggerInput negative) : base(positive, negative, Calculate) { }

      private static float Calculate(bool positive, bool negative) => (positive ? 1 : 0) + (negative ? -1 : 0);
    }

    class CombineScalars : Combinator<float, float, Vector2> {
      public CombineScalars(ScalarInput x, ScalarInput y) : base(x, y, Calculate) { }

      private static Vector2 Calculate(float x, float y) => new Vector2(x, y);
    }
  }
}
