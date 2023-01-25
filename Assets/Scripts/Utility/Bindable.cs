using System;
using System.Collections.Generic;

namespace Utility{
    public class  Bindable<T>{
        
        private class Wrapper{
            public event Action<T> ValueChanged;
            private T _value;
            public T Value{
                set{
                    _value = value;
                    ValueChanged?.Invoke(value);
                }
                get => _value;
            }
        }
        
        private Wrapper _wrapper;
        private readonly List<Action<T>> _listeners = new();

        public Bindable(T init){
            _wrapper = new Wrapper(){Value = init};
            _wrapper.ValueChanged += OnValueChanged;
        }

        public Bindable(Bindable<T> src){
            _wrapper = src._wrapper;
            _wrapper.ValueChanged += OnValueChanged;
        }

        public void Set(T newValue){
            _wrapper.Value = newValue;
        }

        private void OnValueChanged(T newValue){
            foreach (var listener in _listeners){
                listener.Invoke(newValue);
            }
        }

        public void BindSource(Bindable<T> src){
            _wrapper.ValueChanged -= OnValueChanged;
            _wrapper = src._wrapper;
            _wrapper.ValueChanged += OnValueChanged;
        }

        public void Bind(Action<T> binder){
            _listeners.Add(binder);
        }

        public void Unbind(Action<T> binder){
            _listeners.Remove(binder);
        }

        ~Bindable(){
            _wrapper.ValueChanged -= OnValueChanged;
        }

        public static implicit operator T(Bindable<T> other) => other._wrapper.Value;
        public static implicit operator Bindable<T>(T other) => new(other);
    }
}