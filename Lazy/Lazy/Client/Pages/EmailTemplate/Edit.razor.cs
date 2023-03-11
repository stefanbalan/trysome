using System;
using Lazy.Model;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Radzen;

namespace Lazy.Client.Pages.EmailTemplate;

public partial class Edit
{

    [Parameter]
    public int? TemplateId { get; set; }


    private readonly EmailTemplateModel _defaultEmailTemplateModel = new() { Html = true };
    private EmailTemplateModel _emailTemplate = new() { Html = true };

    protected override void OnInitialized()
    {
        //EditContext = new EditContext(person);
        //EditContext.OnFieldChanged += EditContext_OnFieldChanged;

        base.OnInitialized();
    }

    protected override async Task OnParametersSetAsync()
    {
        if (TemplateId is null or 0)
            _emailTemplate = new EmailTemplateModel { Html = true };
        else
            _emailTemplate = await DataService.GetById(TemplateId.Value) ?? _defaultEmailTemplateModel;
    }

    private void GoBack()
    {
        NavigationManager.NavigateTo("/EmailTemplate/List");
    }

    private async Task Save()
    {
        if (!_emailTemplate.IsValid())
        {
            NotificationService.Notify(new NotificationMessage { Severity = NotificationSeverity.Warning, Detail = "Email template model is not valid." });
            return;
        }
        var result = await DataService.CreateOrUpdate(_emailTemplate);
        if (result != null) _emailTemplate = result;
    }


    private void EditContext_OnFieldChanged()
    {
        // _hasChanges = true;
    }
}