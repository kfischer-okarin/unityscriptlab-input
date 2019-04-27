namespace UnityScriptLab {
  namespace Input {
    namespace Event {
      public interface ValueProvider<T> {
        T GetValue(InputSystem input);
      }
    }
  }
}
