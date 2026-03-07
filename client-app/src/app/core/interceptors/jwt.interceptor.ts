import { HttpInterceptorFn } from '@angular/common/http';

export const jwtInterceptor: HttpInterceptorFn = (req, next) => {
  // Retrieve the token from local storage (we will save it here during Login)
  const token = localStorage.getItem('token');

  if(token){
    // Requests are immutable, so we must clone it to modify headers
    const authReq = req.clone({
      setHeaders: {
        Authorization: `Bearer ${token}`
      }
    })
    return next(authReq);
  }
  // If no token, send the request as-is (e.g., for login/register endpoints)
  return next(req);
};
