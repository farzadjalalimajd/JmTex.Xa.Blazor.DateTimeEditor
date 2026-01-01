using DevExpress.ExpressApp.Blazor.Components;
using DevExpress.ExpressApp.Blazor.Components.Models;
using DevExpress.ExpressApp.Blazor.Editors.Adapters;
using GemTex.ExpressApp.Blazor.Editors.Helpers;
using GemTex.ExpressApp.Blazor.Editors.Renderers;
using Microsoft.AspNetCore.Components;

namespace GemTex.ExpressApp.Blazor.Editors.Adapters;

public class PersianDateEditAdapter<T> : DxDateEditAdapter
{
    private DevExpress.ExpressApp.Blazor.Components.Models.Notifier notifier;

    public override DxDateEditModel<T> ComponentModel { get; }

    protected override IComponentModel ObservableModel => notifier;

    public PersianDateEditAdapter(DxDateEditModel<T> componentModel)
    {
        ArgumentNullException.ThrowIfNull(componentModel, "componentModel");
        ComponentModel = componentModel;
        ComponentModel.DateChanged = EventCallback.Factory.Create((object)this, (Action<T>)ComponentModel_DateChanged);
        Type type = Nullable.GetUnderlyingType(typeof(T)) ?? typeof(T);
        Type type2 = type;
        if ((object)type2 == null)
        {
            goto IL_00a1;
        }

        ComponentModelBase componentModelBase;
        if (type2 == typeof(DateOnly))
        {
            componentModelBase = base.DxDateEditMaskProperties.DateOnly;
        }
        else
        {
            Type type3 = type2;
            if (!(type3 == typeof(DateTime)))
            {
                goto IL_00a1;
            }

            componentModelBase = base.DxDateEditMaskProperties.DateTime;
        }

        goto IL_00ae;
    IL_00ae:
        ComponentModelBase componentModelBase2 = componentModelBase;
        notifier = new DevExpress.ExpressApp.Blazor.Components.Models.Notifier(componentModel);
        notifier.Subscribe(componentModelBase2);
        ComponentModel.MaskProperties = componentModelBase2.GetComponentContent();
        return;
    IL_00a1:
        componentModelBase = base.DxDateEditMaskProperties.DateTime;
        goto IL_00ae;
    }

    private void ComponentModel_DateChanged(T date)
    {
        ComponentModel.Date = date;
        RaiseValueChanged();
    }

    public override void SetAllowEdit(bool allowEdit)
    {
        base.SetAllowEdit(allowEdit);
        ComponentModel.Enabled = allowEdit;
    }

    public override void SetAllowNull(bool allowNull)
    {
        ComponentModel.ClearButtonDisplayMode = BlazorCastHelper.GetEditorClearButtonDisplayMode(allowNull);
    }

    public override void SetDisplayFormat(string displayFormat)
    {
        ComponentModel.DisplayFormat = displayFormat;
    }

    public override void SetEditMask(string editMask)
    {
        ComponentModel.Format = editMask;
    }

    public override void SetNullText(string nullText)
    {
        ComponentModel.NullText = nullText;
    }

    public override object GetValue()
    {
#pragma warning disable CS8603 // Possible null reference return.
        return ComponentModel.Date;
#pragma warning restore CS8603 // Possible null reference return.
    }

    public override void SetValue(object value)
    {
#pragma warning disable CS8601 // Possible null reference assignment.
        ComponentModel.Date = (value == null) ? default : ((T)value);
#pragma warning restore CS8601 // Possible null reference assignment.
    }

    protected override RenderFragment CreateComponent()
    {
        //return ComponentModelObserver.Create(ComponentModel, PersianDateEditRenderer<T>.Create(ComponentModel));
        return ComponentModelObserver.Create(notifier, ComponentModelObserver.Create(ConditionalAppearanceModel, ConditionalAppearanceContainer.Create(ConditionalAppearanceModel, GetConditionalAppearanceSelector(), XafValidationMessageContainer.Create(ValidationModel, PersianDateEditRenderer<T>.Create(ComponentModel)))));
    }
}
