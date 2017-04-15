import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpModule } from '@angular/http';

import { CartService } from './shared/services/cart.service';

import { AppRoutingModule } from './app-routing.module';
import { ProductsModule } from './products/products.module';
import { CheckoutModule } from './checkout/checkout.module';

import { AppComponent } from './app.component';
import { AppNavComponent } from './nav/app-nav.component';

import { InternalNotificationService } from './shared/services/internal-notification.service';

import { APP_CONFIG, APP_DI_CONFIG } from './app.config';

@NgModule({
    imports: [
        CommonModule,
        HttpModule,
        ProductsModule,
        CheckoutModule,
        AppRoutingModule
    ],
    declarations: [
        AppComponent,
        AppNavComponent
    ],
    providers: [
        InternalNotificationService,
        CartService,
        { provide: APP_CONFIG, useValue: APP_DI_CONFIG }
    ],
    bootstrap: [
        AppComponent
    ]
})
export class AppModule { }
