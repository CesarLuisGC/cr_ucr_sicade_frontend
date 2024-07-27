import { Routes } from '@angular/router';

export const routes: Routes = [
    {
        path: '',
        loadChildren: () => import('./modules/modules.module').then(m => m.ModulesModule)
    },
];
