import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Products} from '../product.service';

@Injectable({
  providedIn: 'root'
})
export class OrderService {

  constructor(private http: HttpClient) {
  }

  public GetOrders() {
    return this.http.get('http://localhost:63170/api/Order');
  }

  public postOrder(form: any) {
    return this.http.post('http://localhost:63170/api/Order', form);
  }

  public updateOrder(value: any) {
    return this.http.put('http://localhost:63170/api/Order', value);
  }
}

export class Orders {
  id: number;
  ordererName: string;
  orderStatus: string;
  startDate: Date;
  endDate: Date;
  orderedProducts: OrderedProduct[] = [];
}

export class OrderedProduct {
  product: Products;
  countProduct: number;
}

