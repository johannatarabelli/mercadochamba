import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { MaterialModule } from '../../../material/material.module';

@Component({
  selector: 'app-job-dialog',
  standalone: true,
  imports: [MaterialModule],
  templateUrl: './job-dialog.component.html',
  styleUrl: './job-dialog.component.scss'
})
export class JobDialogComponent {
  constructor(
    public dialogRef: MatDialogRef<JobDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public job: any
  ) {}

  onClose(): void {
    this.dialogRef.close();
  }
}
