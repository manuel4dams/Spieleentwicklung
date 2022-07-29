using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;

namespace Project
{
    public abstract class PrefsHandlerAbstract<T> : MonoBehaviour
    {
        public Prefs key;
        public UnityEvent<T> onStart;

        void Start()
        {
            onStart?.Invoke(GetPrefs(key));
        }

        [UsedImplicitly]
        public void Set(T value)
        {
            SetPrefs(key, value);
        }

        protected abstract T GetPrefs(Prefs prefs);
        protected abstract void SetPrefs(Prefs prefs, T value);
    }
}
