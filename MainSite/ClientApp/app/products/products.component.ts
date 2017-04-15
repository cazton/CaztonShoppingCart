import { Component, OnInit } from '@angular/core';
import { Observable } from "rxjs/Rx";

import {ProductService} from "./product.service";
import { Product } from "./models/product";

@Component({
    template: `
        <h3>Add Products To Your Cart</h3>
        
        <div class="row">
            <div class="col-md-4 product-wrapper" *ngFor="let product of products | async">
                <product-info [productInfo]="product"></product-info>
            </div>
        </div>
    `,
    styles: ['.product-wrapper { padding-top: 10px; padding-bottom: 10px; }']
})
export class ProductsComponent implements OnInit{
    products: Observable<Product[]>;

    constructor(private productService: ProductService){}

    ngOnInit(){
        this.products = this.productService.getProducts();
    }
}