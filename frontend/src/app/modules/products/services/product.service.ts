import { Injectable } from "@angular/core";
import { of } from "rxjs";
import { PRODUCTS } from "../data/products.data";


@Injectable()
export class ProductService {
    
    public getProducts(){
        return of(PRODUCTS)
    }
}