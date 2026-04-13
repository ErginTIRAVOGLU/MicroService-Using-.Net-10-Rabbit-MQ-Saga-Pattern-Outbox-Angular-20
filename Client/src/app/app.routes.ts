import { Routes } from '@angular/router';

export const routes: Routes = [
    {
        path:'', loadComponent: () => import('./home/home').then(m => m.Home)
    },
    {
        path:'store', loadComponent: () => import('./store/store/store').then(m => m.Store)
    },
    {
        path:'server-error', loadComponent: () => import('./core/server-error/server-error').then(m => m.ServerError)
    }
    ,
    {
        path:'unauthenticated', loadComponent: () => import('./core/un-authenticated/un-authenticated').then(m => m.UnAuthenticated)
    },
    {
        path:'**', loadComponent: () => import('./core/not-found/not-found').then(m => m.NotFound)
    }

];
