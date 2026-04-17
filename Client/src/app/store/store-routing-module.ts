import { RouterModule, type Routes } from "@angular/router";
import { Store } from "./store/store";
import { NgModule } from "@angular/core";
import { authGuard } from "../auth/guards/auth.guard";

const routes: Routes = [
    { path: '', loadComponent: () => import('./store/store').then(m => m.Store)},
    { path: 'product/:id', loadComponent: () => import('./product-details/product-details').then(m => m.ProductDetails)},
    { path: 'basket', loadComponent: () => import('./basket/basket').then(m=>m.BasketComponent)},
    { path: 'checkout', loadComponent: () => import('./checkout/checkout').then(m=>m.Checkout),canActivate:[authGuard]},
    { path: 'checkout-success', loadComponent: () => import('./checkout-success/checkout-success').then(m=>m.CheckoutSuccess),canActivate:[authGuard]},
    { path: 'orders', loadComponent: () => import('./orders/orders').then(m=>m.Orders),canActivate:[authGuard]}
]

@NgModule({
    imports:[RouterModule.forChild(routes)],
    exports: [RouterModule]
})

export class StoreRoutingModule {}