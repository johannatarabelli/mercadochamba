using backnc.Common.DTOs.CategoryDTO;
using FluentValidation;

namespace backnc.Common.Validators
{
	public class CategoryDTOValidator : AbstractValidator<CategoryDTO>
	{
		public CategoryDTOValidator()
		{
			RuleFor(category => category.Name)
				.NotEmpty().WithMessage("El nombre de la categoría es obligatorio.")
				.MaximumLength(50).WithMessage("El nombre de la categoría no puede tener más de 50 caracteres.");
		}
	}
}
