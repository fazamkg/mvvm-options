using UnityEngine;

public class EntryPoint : MonoBehaviour
{
	[SerializeField] private OptionsView _optionsView;

	private void Awake()
	{
		var model = new OptionsModel();
		var viewModel = new OptionsViewModel(model);
		_optionsView.Init(viewModel);
	}
}
