import {Product} from "../../products/models/product";

export class Cart {
    id: string;
    products: Product[];
    shipping: number;
    tax: string;

    constructor(id: string){
        this.id = id;
    }
}
