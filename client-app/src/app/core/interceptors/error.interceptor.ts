import { HttpInterceptorFn, HttpErrorResponse } from '@angular/common/http';
import { catchError, throwError } from 'rxjs';

export const errorInterceptor: HttpInterceptorFn = (req, next) => {
  return next(req).pipe(
    catchError((error: HttpErrorResponse) => {
      let errorMessage = 'An unexpected server error occurred.';

      // Check if it's our standard .NET ProblemDetails format
      if (error.error && error.error.detail) {
        errorMessage = error.error.detail;
      } else if (error.error && error.error.title) {
        errorMessage = error.error.title;
      }

      // Senior Note: In a production app, you would inject a Toastr/Snackbar service here.
      // For rapid feedback right now, we will use a browser alert so you can instantly
      // see the backend validation working when you test the UI.
      if (error.status !== 401) { // We handle 401s differently (usually redirect to login)
        alert(`API Error: ${errorMessage}`);
      }

      return throwError(() => new Error(errorMessage));
    })
  );
};