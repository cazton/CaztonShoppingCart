import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Rx'
import { Subject } from "rxjs/Subject";

import {InternalNotification, InternalNotificationType} from '../models/internal-notification';

@Injectable()
export class InternalNotificationService{
    internalNotifications: Observable<InternalNotification>;

    private internalNotificationSubject = new Subject<InternalNotification>();

    constructor(){
        this.internalNotifications = this.internalNotificationSubject.asObservable();
    }

    notify(messageType: InternalNotificationType, data: any){
        this.internalNotificationSubject.next(new InternalNotification(messageType, data));
    }
}