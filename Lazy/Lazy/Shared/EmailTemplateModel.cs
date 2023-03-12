using FluentValidation;

namespace Lazy.Model;

public record EmailTemplateModel

{
 

    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
    public bool Html { get; set; }

}


public class EmailTemplateModelValidator : AbstractValidator<EmailTemplateModel>
{
    public EmailTemplateModelValidator()
    {
        RuleFor(et => et.Name)
            .NotNull().NotEmpty()
            .MinimumLength(3).MaximumLength(50);
    }
}