import { inject, Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';

@Injectable({
  providedIn: 'root'
})
export class SnackbarService {

  private snackBar = inject(MatSnackBar);

  openSnackBar(message: string, panelClass: string) {
    this.snackBar.open(message, 'Cerrar', {
      duration: 5000,
      panelClass: [panelClass],
    });
  }

  openSuccessSnackBar(message: string) {
    this.openSnackBar(message, 'success-snackbar');
  }

  openErrorSnackBar(message: string) {
    this.openSnackBar(message, 'error-snackbar');
  }

  openInfoSnackBar(message: string) {
    this.openSnackBar(message, 'info-snackbar');
  }

  openWarningSnackBar(message: string) {
    this.openSnackBar(message, 'warning-snackbar');
  }
}
