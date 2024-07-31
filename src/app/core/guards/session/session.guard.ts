import { ActivatedRouteSnapshot, CanActivateFn, Router, RouterStateSnapshot } from '@angular/router';
import { SesionService } from '../../services/sesion/sesion.service';
import { inject } from '@angular/core';

export const SessionGuard: CanActivateFn = (
  next: ActivatedRouteSnapshot,
  state: RouterStateSnapshot
) => {
  const _session: SesionService = inject(SesionService);
  const _router: Router = inject(Router);

  if (_session.IsAuthenticated()) {
    return true;
  }
  
  _router.navigate(['/']);

  return false;
};
