public class OptionsViewModel
{
	public ValidatableReactiveProperty<float> Sensitivity { get; private set; } = new();
	public ReactiveProperty<bool> VsyncOn { get; private set; } = new();
	public ReactiveProperty<TextureQuality> TextureQuality { get; private set; } = new();
	public ValidatableReactiveProperty<string> Name { get; private set; } = new();

	private readonly OptionsModel _model;

	public OptionsViewModel(OptionsModel model)
	{
		_model = model;

		Sensitivity.AddValidation(value => value >= 0f && value <= 1f);
		Name.AddValidation(value => value == null || value.Length < 10);

		LoadAll();
	}

	public void SaveAll()
	{
		Sensitivity.AssignTo(_model.Sensitivity);
		VsyncOn.AssignTo(_model.VsyncOn);
		TextureQuality.AssignTo(_model.TextureQuality);
		Name.AssignTo(_model.Name);
	}

	public void LoadAll()
	{
		_model.Sensitivity.AssignTo(Sensitivity);
		_model.VsyncOn.AssignTo(VsyncOn);
		_model.TextureQuality.AssignTo(TextureQuality);
		_model.Name.AssignTo(Name);
	}
}
