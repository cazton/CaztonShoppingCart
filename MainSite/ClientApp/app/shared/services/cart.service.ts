import {Injectable, Inject} from '@angular/core';
import { Http, Response } from '@angular/http';
import { Observable } from 'rxjs/Rx';
import 'rxjs/add/operator/map';

import { Cart } from "../models/cart";

import { APP_CONFIG, AppConfig } from '../../app.config';

@Injectable()
export class CartService {
    //CartService is a singleton, so this is another way to pass cart around
    cart: Cart;

    constructor(private http: Http, @Inject(APP_CONFIG) private config: AppConfig){}

    getCart(): Observable<Cart> {
        return this.http.get(`/api/Cart/${this.cart.id}`)
            .map((response: Response) => {
                var cart = response.json() as Cart;

                this.cart = cart;
                return cart;
            });
    }

    initializeCart(): Observable<any> {
        return this.http.post(`${this.config.apiEndpoint}/api/Cart`, null)
            .map((response: Response) => {
                var cartId = response.json() as string;
                this.cart = new Cart(cartId);
                return null;
            })
            .catch(err => Observable.throw('This is an error'));
    }

    addItem(itemId: string): Observable<string> {
        return this.http.put(`/api/Cart/${this.cart.id}/item/${itemId}`, null)
            .map(() => { return null })
            .catch(err => Observable.throw('This is an error'));
    }

    removeItem(itemId: string): Observable<any> {
        return this.http.delete(`/api/Cart/${this.cart.id}/item/${itemId}`)
            .map(() => { return null })
            .catch(err => Observable.throw('This is an error'));
    }
}