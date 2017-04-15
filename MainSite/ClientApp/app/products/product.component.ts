import { Component, Input } from '@angular/core';

import { CartService } from "../shared/services/cart.service";
import { InternalNotificationService } from "../shared/services/internal-notification.service";
import { InternalNotificationType } from "../shared/models/internal-notification";
import { Product } from "./models/product";

@Component({
    selector: 'product-info',
    template: `
        <div class="row product-image">
            <div class="col-md-12">
                <img src = "images/{{productInfo.image}}-300.png" class='pull-left' />
            </div>
        </div>

        <div class='row title' style="">
            <div class='col-md-4'>
                <label>{{productInfo.name}}</label>
            </div>
            <div class='col-md-4'>
                <label>{{productInfo.price | currency:'USD':true:'1.2-2'}}</label>
            </div>
            <div class='col-md-4'>
                <button class="btn btn-xs btn-success btn-block" (click)="addItem(productInfo.id)">Add To Cart</button>
            </div>
        </div>
    `,
    styles: ['.product-image { padding-bottom: 10px; }']
})
export class ProductComponent {
    @Input() productInfo: Product;

    constructor(private cartService: CartService, private internalNotificationService: InternalNotificationService) { }

    addItem(id: string) {
        this.cartService.addItem(id)
            .subscribe(
                () => {
                    this.internalNotificationService.notify(InternalNotificationType.CartUpdated, null);
                }, //do something here for success
                error => {
                    console.log('error: ', error);
                }
            );
    }
}