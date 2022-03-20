using FluentValidation;
using LetsCode.DTO;
using LetsCode.Models;

namespace LetsCode.Validators;

public class CardDtoValidator : AbstractValidator<CardDTO>
{
    public CardDtoValidator()
    {
        RuleFor(card => card.Title).NotEmpty().WithMessage("Título obrigatório");
        RuleFor(card => card.Content).NotEmpty().WithMessage("Conteúdo obrigatório");
        RuleFor(card => card.List).IsEnumName(typeof(KanbanListEnum), false).WithMessage("Deve estar em uma lista válida: TODO, DOING ou DONE");
    }
}

public class CreateCardDtoValidator : AbstractValidator<CreateCardDTO>
{
    public CreateCardDtoValidator()
    {
        RuleFor(card => card.Title).NotEmpty().WithMessage("Título obrigatório");
        RuleFor(card => card.Content).NotEmpty().WithMessage("Conteúdo obrigatório");
    }
}