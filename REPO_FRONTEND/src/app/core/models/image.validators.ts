import { AbstractControl, ValidationErrors, ValidatorFn } from '@angular/forms';

export function imageValidator(): ValidatorFn {
  return (control: AbstractControl): ValidationErrors | null => {
    const file = control.value as File;
    if (file && !file.type.startsWith('image/')) {
      return { invalidType: true };
    }
    return null;
  };
}