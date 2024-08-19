using backnc.Models;
using FluentValidation;

namespace backnc.Data.ConfigEntities
{
	public class RegisterUserValidator : AbstractValidator<RegisterUser>
	{
		public RegisterUserValidator(IUserValidationService userValidationService)
		{
			RuleFor(x => x.userName)
				.NotEmpty().WithMessage("El nombre de usuario es obligatorio.")
				.Length(3, 50).WithMessage("El nombre de usuario debe tener entre 3 y 50 caracteres.")
				.MustAsync(async (userName, cancellation) => !await userValidationService.IsUserNameTaken(userName))
				.WithMessage("El nombre de usuario ya está tomado.");

			RuleFor(x => x.email)
				.NotEmpty().WithMessage("El email es obligatorio.")
				.EmailAddress().WithMessage("El email no es válido.")
				.Length(1, 100).WithMessage("El email debe tener un máximo de 100 caracteres.")
				.MustAsync(async (email, cancellation) => !await userValidationService.IsEmailTaken(email))
				.WithMessage("El email ya está en uso.");

			RuleFor(x => x.dni)
				.NotEmpty().WithMessage("El dni es obligatorio.")
				.Length(7, 15).WithMessage("El dni debe tener entre 7 y 15 caracteres.")
				.MustAsync(async (dni, cancellation) => !await userValidationService.IsDniTaken(dni))
				.WithMessage("El dni ya está en uso.");

			RuleFor(x => x.firstName)
				.NotEmpty().WithMessage("El nombre es obligatorio.")
				.Length(1, 50).WithMessage("El nombre debe tener entre 1 y 50 caracteres.");

			RuleFor(x => x.lastName)
				.NotEmpty().WithMessage("El apellido es obligatorio.")
				.Length(1, 50).WithMessage("El apellido debe tener entre 1 y 50 caracteres.");

			RuleFor(x => x.address)
				.NotEmpty().WithMessage("La dirección es obligatoria.")
				.Length(5, 100).WithMessage("La dirección debe tener entre 5 y 100 caracteres.");

			RuleFor(x => x.phoneNumber)
				.NotEmpty().WithMessage("El número de teléfono es obligatorio.")
				.Length(7, 15).WithMessage("El número de teléfono debe tener entre 7 y 15 caracteres.");

			RuleFor(x => x.password)
				.NotEmpty().WithMessage("La contraseña es obligatoria.")
				.MinimumLength(8).WithMessage("La contraseña debe tener al menos 8 caracteres.")
				.Matches("[A-Z]").WithMessage("La contraseña debe contener al menos una letra mayúscula.")
				.Matches("[^a-zA-Z0-9]").WithMessage("La contraseña debe contener al menos un carácter especial.");
		}
	}
}
