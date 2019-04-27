namespace UnityScriptLab {
  namespace Input {
    namespace Value {
      public interface ValueProvider<T> {
        T GetValue(InputSystem input);
      }
    }
  }
}
