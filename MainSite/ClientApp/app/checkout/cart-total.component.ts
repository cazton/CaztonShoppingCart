import { Component, Input, OnInit } from '@angular/core';

import { Cart } from "../shared/models/cart";
import { Product } from "../products/models/product";

@Component({
    selector: 'cart-total',
    templateUrl: './cart-total.component.html',
    styles: ['.cart-totals {padding: 20px; background-color: #ccc; border: 1px solid black; border-radius: 4px; -webkit-border-radius: 4px; -moz-border-radius: 4px;}']
})
export class CartTotalComponent implements OnInit {
    @Input() cart: Cart;

    cartPrices: { subtotal: number, tax: number, shipping: number } =
        { subtotal: 0, tax: 0, shipping: 0 };

    ngOnInit() {
        this.getSubtotal();
        this.getTax(this.cartPrices.subtotal);
        this.cartPrices.shipping = 20;
    }

    private getSubtotal() {
        this.cart.products.forEach((product: Product) => {
            if (this.cartPrices.subtotal)
                this.cartPrices.subtotal += product.price;

            this.cartPrices.subtotal = product.price;
        });
    }

    private getTax(subtotal: number) {
        this.cartPrices.tax = subtotal * 0.085;
    }
}