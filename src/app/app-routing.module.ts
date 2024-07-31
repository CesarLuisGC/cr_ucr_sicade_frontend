import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { SessionGuard } from './core/guards/session/session.guard';
import { LoginGuard } from './core/guards/login/login.guard';

export const routes: Routes = [
  {
    path: 'auth',
    canActivate: [LoginGuard],
    loadChildren: () =>
      import('./modules/auth/auth.module').then((m) => m.AuthModule),
  },
  {
    path: 'error',
    loadChildren: () =>
      import('./modules/errors/errors.module').then((m) => m.ErrorsModule),
  },
  {
    path: 'original',
    loadChildren: () => import('./modules/modules.module').then((m) => m.ModulesModule),
  },
  {
    path: '',
    canActivate: [SessionGuard],
    loadChildren: () =>
      import('./_metronic/layout/layout.module').then((m) => m.LayoutModule),
  },
  { path: '**', redirectTo: 'error/404' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
