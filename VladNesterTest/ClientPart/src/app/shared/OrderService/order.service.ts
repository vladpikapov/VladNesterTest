import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Products} from '../product.service';

@Injectable({
  providedIn: 'root'
})
export class OrderService {

  constructor(private http: HttpClient) { }

  public GetOrders() {
    return this.http.get('http://localhost:63170/api/Order');
  }

  public postOrder(form: any) {
    return this.http.post('http://localhost:63170/api/Order', form);
  }

}



export interface Orders {
  id: number;
  ordererName: string;
  orderStatus: string;
  productCount: number;
  startDate: Date;
  endDate: Date;
  orderedProducts: OrderedProduct[] ;

}

export  interface OrderedProduct {
  product: Products;
  countProduct: number;
}
