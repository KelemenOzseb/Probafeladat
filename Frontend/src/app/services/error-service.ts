import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ErrorService {
  error = new BehaviorSubject<string | null>(null)
  error$ = this.error.asObservable()
  
  showError(message: string) {
    this.error.next(message)
  }

  clearError() {
    this.error.next(null)
  }
}
