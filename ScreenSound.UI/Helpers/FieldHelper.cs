using MudBlazor;
using ScreenSound.UI.Components;
using static MudBlazor.CategoryTypes;

namespace ScreenSound.UI.Helpers;

public static class FieldHelper
{
	public static async Task<bool> EntityFound(object? entity, string entityName, IDialogService Dialog)
	{
		if (entity is null)
		{
			var parameters = new DialogParameters
				{
					{ "DialogTitle", "Loading error" },
					{ "DialogContent", $"Unable to load {entityName} properly." }
				};

			var options = new DialogOptions
			{
				CloseOnEscapeKey = true,
				MaxWidth = MaxWidth.ExtraSmall
			};

			var dialog = await Dialog.ShowAsync<SimpleDialog>("Loading error", parameters, options);
			await dialog.Result;

			return false;
		}

		return true;
	}
}
