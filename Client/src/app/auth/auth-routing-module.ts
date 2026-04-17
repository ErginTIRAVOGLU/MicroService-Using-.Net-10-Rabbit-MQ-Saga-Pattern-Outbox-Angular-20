import { NgModule } from "@angular/core";
import { RouterModule, type Routes } from "@angular/router";


const routes: Routes = [
    { path: 'login', loadComponent: () => import('./login/login').then(m => m.Login) },
    { path: 'register', loadComponent: () => import('./register/register').then(m => m.Register) },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})

export class AuthRoutingModule {}

