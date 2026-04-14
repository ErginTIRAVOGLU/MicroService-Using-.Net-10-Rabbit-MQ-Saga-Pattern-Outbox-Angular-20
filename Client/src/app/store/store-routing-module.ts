import { RouterModule, type Routes } from "@angular/router";
import { Store } from "./store/store";
import { NgModule } from "@angular/core";

const routes: Routes = [
    { path: '', loadComponent: () => import('./store/store').then(m => m.Store)},
    { path: 'product/:id', loadComponent: () => import('./product-details/product-details').then(m => m.ProductDetails)},
]

@NgModule({
    imports:[RouterModule.forChild(routes)],
    exports: [RouterModule]
})

export class StoreRoutingModule {}