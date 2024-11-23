public enum TextureQuality
{
	Low,
	Medium,
	High
}

public class OptionsModel
{
	public ReactiveProperty<float> Sensitivity { get; private set; } = new();
	public ReactiveProperty<bool> VsyncOn { get; private set; } = new();
	public ReactiveProperty<TextureQuality> TextureQuality { get; private set; } = new();
	public ReactiveProperty<string> Name { get; private set; } = new();

	public OptionsModel()
	{
		Sensitivity.Value = 0.5f;
		VsyncOn.Value = true;
		TextureQuality.Value = global::TextureQuality.Medium;
		Name.Value = "Freeman";
	}
}
