import { Component } from '@angular/core';
import { VatFormComponent } from './vat-form/vat-form.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [VatFormComponent],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {}