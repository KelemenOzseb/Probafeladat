import { HttpErrorResponse, HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { catchError, throwError } from 'rxjs';
import { ErrorService } from './services/error-service';

export const errorInterceptorInterceptor: HttpInterceptorFn = (req, next) => {
  const errorService = inject(ErrorService)
  return next(req).pipe(
    catchError((err: HttpErrorResponse) => {
      let userMessage = "Ismeretlen hiba történt."

      if (err.status === 0) {
        userMessage = "Hálózati hiba: a szerver nem elérhető (CORS vagy offline)."
      } 
      else if (err.status === 400) {
        userMessage = "Hibás kérés"
      }
      else if (err.status === 404) {
        userMessage = "Az erőforrás nem található."
      } else if (err.status >= 500) {
        userMessage = "Szerverhiba történt. Próbáld meg később."
      } else if (err.error?.message || err.error?.error) {
        userMessage = err.error.message ?? err.error.error
      }
      errorService.showError(userMessage)
      
      console.error("[HTTP ERROR]", {
        method: req.method,
        url: req.urlWithParams,
        status: err.status,
        error: err
      });
      (err as any).userMessage = userMessage
      return throwError(() => err) 
    })
    
  )
};
