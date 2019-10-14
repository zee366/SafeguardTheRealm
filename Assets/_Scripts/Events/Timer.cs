using UnityEngine;
using UnityEngine.Events;

namespace Events {
    public class Timer : MonoBehaviour {

        [SerializeField] private bool  startOnAwake = true;
        [SerializeField] private float duration     = 1f;
        [SerializeField] private bool  loopingTimer = false;

        public UnityEvent onStart;
        public UnityEvent onFinish;
        public UnityEvent onStop;
        public UnityEvent onReset;

        private bool  _running = false;
        private float _timer   = 0f;


        private void Start() {
            if ( startOnAwake )
                StartTimer();
        }


        private void Update() {
            if ( !_running )
                return;

            _timer += Time.deltaTime;
            if ( _timer >= duration )
                FinishTimer();
        }


        public void StartTimer() {
            _running = true;
            onStart?.Invoke();
        }


        public void StopTimer() {
            _running = false;
            onStop?.Invoke();
        }


        public void FinishTimer() {
            StopTimer();
            onFinish?.Invoke();

            if ( loopingTimer )
                ResetTimer();
        }


        public void ResetTimer() {
            _timer = 0f;
            onReset?.Invoke();
        }

    }
}