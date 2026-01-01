using DevExpress.Blazor;

namespace GemTex.ExpressApp.Blazor.Editors.Helpers;

internal static class BlazorCastHelper
{
    internal static DataEditorClearButtonDisplayMode GetEditorClearButtonDisplayMode(bool allowClear)
    {
        if (!allowClear)
        {
            return DataEditorClearButtonDisplayMode.Never;
        }

        return DataEditorClearButtonDisplayMode.Auto;
    }
}