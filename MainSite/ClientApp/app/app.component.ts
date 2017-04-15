import { Component, OnInit } from '@angular/core';
import {CartService} from "./shared/services/cart.service";

@Component({
    selector: 'app',
    templateUrl: './app.component.html',
    styleUrls: ['./app-styles.css']
})
export class AppComponent implements OnInit {
    constructor(private cartService: CartService){}

    ngOnInit(){
        //initialize our cart
        this.cartService.initializeCart().subscribe();
    }
}
