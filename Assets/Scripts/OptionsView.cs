using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Collections.Generic;
using System.Collections;

public class OptionsView : MonoBehaviour
{
	[SerializeField] private Slider _sensitivitySlider;
	[SerializeField] private TMP_Text _sensitivityText;
	[SerializeField] private Toggle _vsyncToggle;
	[SerializeField] private TMP_Dropdown _textureQualityDropdown;
	[SerializeField] private TMP_InputField _nameInputField;
	[SerializeField] private Button _saveButton;
	[SerializeField] private Button _revertButton;

	private OptionsViewModel _viewModel;
	private readonly List<Action> _onDestroyFunctions = new();

	public void Init(OptionsViewModel viewModel)
	{
		_viewModel = viewModel;

		BindSlider(_viewModel.Sensitivity, _sensitivitySlider, _sensitivityText);
		BindToggle(_viewModel.VsyncOn, _vsyncToggle);
		BindDropdown(_viewModel.TextureQuality, _textureQualityDropdown);
		BindInputField(_viewModel.Name, _nameInputField);

		_saveButton.onClick.AddListener(OnSaveButtonClick);
		_revertButton.onClick.AddListener(OnRevertButtonClick);

		StartCoroutine(WithDelayCoroutine());
	}

	private IEnumerator WithDelayCoroutine()
	{
		yield return null;
		LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)transform);
	}

	private void OnDestroy()
	{
		foreach (var function in _onDestroyFunctions)
		{
			function?.Invoke();
		}
	}

	private void BindSlider(ReactiveProperty<float> property, Slider slider, TMP_Text text)
	{
		void OnSlider(float value)
		{
			property.Value = value;
		}

		slider.onValueChanged.AddListener(OnSlider);

		void OnChange(float value)
		{
			slider.value = value;
			text.text = value.ToString();
		}

		property.OnChanged += OnChange;

		_onDestroyFunctions.Add(() => property.OnChanged -= OnChange);

		OnChange(property.Value);
	}

	private void BindToggle(ReactiveProperty<bool> property, Toggle toggle)
	{
		void OnToggle(bool value)
		{
			property.Value = value;
		}

		toggle.onValueChanged.AddListener(OnToggle);

		void OnChange(bool value)
		{
			toggle.isOn = value;
		}

		property.OnChanged += OnChange;

		_onDestroyFunctions.Add(() => property.OnChanged -= OnChange);

		OnChange(property.Value);
	}

	private void BindDropdown<T>(ReactiveProperty<T> property, TMP_Dropdown dropdown) where T : Enum
	{
		void OnDropdown(int value)
		{
			property.Value = (T)Enum.ToObject(typeof(T), value);
		}

		dropdown.onValueChanged.AddListener(OnDropdown);

		void OnChange(T value)
		{
			dropdown.value = Convert.ToInt32(value);
		}

		property.OnChanged += OnChange;

		_onDestroyFunctions.Add(() => property.OnChanged -= OnChange);

		OnChange(property.Value);
	}

	private void BindInputField(ReactiveProperty<string> property, TMP_InputField field)
	{
		void OnField(string value)
		{
			property.Value = value;
		}

		field.onValueChanged.AddListener(OnField);

		void OnChange(string value)
		{
			field.text = value;
		}

		property.OnChanged += OnChange;

		_onDestroyFunctions.Add(() => property.OnChanged -= OnChange);

		OnChange(property.Value);
	}

	private void OnSaveButtonClick()
	{
		_viewModel.SaveAll();
	}

	private void OnRevertButtonClick()
	{
		_viewModel.LoadAll();
	}
}
