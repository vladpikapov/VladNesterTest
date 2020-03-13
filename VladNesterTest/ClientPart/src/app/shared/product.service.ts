import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  constructor(private http: HttpClient) { }

  public getProducts() {
    return this.http.get('http://localhost:63170/api/Product');
  }

  public postProducts(product: Products) {
    return this.http.post('http://localhost:63170/api/Product', product);
  }

  public addProductOneValue(id: number) {
    return this.http.put('http://localhost:63170/api/Product/add/' + id, null);
  }

  public dropProductOneValue(id: number) {
    return this.http.put('http://localhost:63170/api/Product/drop/' + id, null);
  }

}

export interface Products {
  id: number;
  name: string;
  type: string;
  country: string;
  count: number;
}
