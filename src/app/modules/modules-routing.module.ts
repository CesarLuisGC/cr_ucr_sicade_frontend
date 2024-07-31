import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SidebarComponent } from './private/components/sidebar/sidebar.component';

const routes: Routes = [
  {
    path: 'app',
    component: SidebarComponent,
    loadChildren: () => import('./private/private.module').then(m => m.PrivateModule) 
  },
  {
    path: '', 
    loadChildren: () => import('./public/public.module').then(m => m.PublicModule) 
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ModulesRoutingModule { }