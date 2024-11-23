using System.Collections.Generic;
using System;

public class ValidatableReactiveProperty<T> : ReactiveProperty<T>
{
	private readonly List<Func<T, bool>> _validationRules = new();

	public void AddValidation(Func<T, bool> rule)
	{
		_validationRules.Add(rule);
	}

	protected override void OnValueChanged(T oldValue, T newValue)
	{
		base.OnValueChanged(oldValue, newValue);

		foreach (var rule in _validationRules)
		{
			var isGood = rule(newValue);
			if (isGood == false)
			{
				_value = oldValue;
				return;
			}
		}
	}
}
