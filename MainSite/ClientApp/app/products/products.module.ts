import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ProductsRoutingModule } from './products-routing.module';
import { ProductComponent } from './product.component';
import { ProductsComponent } from './products.component';
import { ProductService } from './product.service';

@NgModule({
    imports: [
        CommonModule,
        ProductsRoutingModule
    ],
    declarations: [
        ProductComponent,
        ProductsComponent
    ],
    providers: [
        ProductService

    ]
})
export class ProductsModule {}
