import {INotificationMessage} from "../interfaces";

export class InternalMessage implements INotificationMessage{
    title: string;
    message: string;
    contentType: string;
    content: any;
    priority: number;

    constructor(priority: number, message: string, title: string){
        this.priority = priority;
        this.message = message;
        this.title = title;
    }
}

export enum InternalNotificationType {
    CartUpdated
}

export class InternalNotification{
    messageType: InternalNotificationType;
    data: any;

    constructor(messageType: InternalNotificationType, data: any){
        this.messageType = messageType;
        this.data = data;
    }
}