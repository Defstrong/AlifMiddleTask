using BusinessLogic;
using FluentValidation;
namespace Presentation;

public class ToUpValidator : AbstractValidator<ToUpDto>
{
    public ToUpValidator()
    {
        RuleFor(toUp => toUp.Quantity).GreaterThan(0);
        RuleFor(toUp => toUp.WalletId).Length(min: 1, max: 50);
    }
}
