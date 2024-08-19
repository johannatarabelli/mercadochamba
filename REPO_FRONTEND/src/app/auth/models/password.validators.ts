import { AbstractControl, ValidationErrors, ValidatorFn } from '@angular/forms';

export function passwordValidator(): ValidatorFn {
  return (control: AbstractControl): ValidationErrors | null => {
    const value = control.value;
    if (!value) {
      return null;
    }

    const hasUpperCase = /[A-Z]/.test(value);
    const hasSpecialCharacter = /[!@#$%^&*(),.?":{}|<>]/.test(value);
    const hasMinimumLength = value.length >= 8;

    const errors: ValidationErrors = {};

    if (!hasUpperCase) {
      errors['noUpperCase'] =
        'La contraseña debe contener al menos una letra mayúscula.';
    }

    if (!hasSpecialCharacter) {
      errors['noSpecialCharacter'] =
        'La contraseña debe contener al menos un carácter especial.';
    }

    if (!hasMinimumLength) {
      errors['minLength8'] = 'La contraseña debe tener al menos 8 caracteres.';
    }

    return Object.keys(errors).length ? errors : null;
  };
}
