import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CheckoutRoutingModule } from './checkout-routing.module';
import { CheckoutComponent } from './checkout.component';
import { CartTotalComponent } from './cart-total.component';

@NgModule({
    imports: [
        CommonModule,
        CheckoutRoutingModule
    ],
    declarations: [
        CheckoutComponent,
        CartTotalComponent
    ]
})
export class CheckoutModule { }
