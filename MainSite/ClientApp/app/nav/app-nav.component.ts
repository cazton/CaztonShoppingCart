import { Component, OnInit, OnDestroy } from '@angular/core';

import { CartService } from "../shared/services/cart.service";
import { InternalNotificationService } from "../shared/services/internal-notification.service";
import { INotificationMessage } from '../shared/interfaces';
import { InternalNotification, InternalNotificationType } from "../shared/models/internal-notification";
import { Cart } from "../shared/models/cart";

@Component({
    selector: 'app-nav',
    templateUrl: './app-nav.component.html',
    styleUrls: ['./../app-styles.css', './app-nav.component.css']
})
export class AppNavComponent implements OnInit, OnDestroy {
    notifications: INotificationMessage[] = [];
    subscriptions: any[] = [];
    cartItemCount: number = 0;

    constructor(private internalNotificationService: InternalNotificationService, private cartService: CartService) {}

    ngOnInit() {
        //subscribe to the internal service
        this.subscriptions.push(this.internalNotificationService.internalNotifications
            .subscribe((internalNotification: InternalNotification) => {
                if (internalNotification.messageType === InternalNotificationType.CartUpdated) {
                    this.getCartItemCount();
                }
            }
            ));
    };

    ngOnDestroy() {
        if (this.subscriptions.length > 0)
            this.subscriptions.forEach(s => s.unsubscribe());
    }

    private getCartItemCount() {
        this.cartService.getCart().subscribe((cart: Cart) => this.cartItemCount = cart.products.length);
    }
}