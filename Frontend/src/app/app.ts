import { Component, signal } from '@angular/core';
import { ErrorService } from './services/error-service';

@Component({
  selector: 'app-root',
  templateUrl: './app.html',
  standalone: false,
  styleUrl: './app.sass'
})
export class App {
  protected readonly title = signal('Frontend');

  constructor(public errorService: ErrorService) {}
}
