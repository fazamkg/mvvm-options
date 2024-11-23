using System;

public class ReactiveProperty<T>
{
	public event Action<T> OnChanged;

	protected T _value;

	public T Value
	{
		get => _value;

		set
		{
			var oldValue = _value;
			_value = value;
			OnValueChanged(oldValue, _value);
			OnChanged?.Invoke(_value);
		}
	}

	public void AssignTo(ReactiveProperty<T> to)
	{
		to.Value = Value;
	}

	protected virtual void OnValueChanged(T oldValue, T newValue)
	{

	}
}
