import { Component } from '@angular/core';

import { CartService } from "../shared/services/cart.service";
import { Cart } from "../shared/models/cart";

@Component({
    templateUrl: './checkout.component.html'
})
export class CheckoutComponent {
    cart: Cart;

    constructor(private cartService: CartService) { }

    ngOnInit() {
        this.cartService.getCart().subscribe((cart: Cart) => this.cart = cart);
    }

    getImage(imageName: string): string {
        return `images/${imageName}-100.png`;
    }
}