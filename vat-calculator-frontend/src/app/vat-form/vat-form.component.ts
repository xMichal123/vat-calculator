import { Component } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';

@Component({
  selector: 'app-vat-form',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatButtonModule,
    MatCardModule
  ],
  templateUrl: './vat-form.component.html',
  styleUrls: ['./vat-form.component.css']
})
export class VatFormComponent {
  vatForm: FormGroup;
  result: any = null;
  error: string = '';

  constructor(private fb: FormBuilder, private http: HttpClient) {
    this.vatForm = this.fb.group({
      netAmount: [null],
      grossAmount: [null],
      vatAmount: [null],
      vatRate: [20]
    });
  }

  onSubmit() {
    const values = this.vatForm.value;
    const filled = ['netAmount', 'grossAmount', 'vatAmount'].filter(k => values[k] != null);

    if (filled.length !== 1) {
      this.error = 'Please provide exactly one amount (Net, Gross, or VAT).';
      this.result = null;
      return;
    }

    this.http.post('http://localhost:5245/api/vatcalculator/calculate', values)
      .subscribe({
        next: (res) => {
          this.result = res;
          this.error = '';
        },
        error: (err) => {
          this.result = null;
          this.error = err.error?.error || 'API error occurred.';
        }
      });
  }
}