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

  public deleteProducts(id: number) {
    return this.http.delete('http://localhost:63170/api/Product/' + id);
  }
}

export interface Products {
  id: number;
  name: string;
  type: string;
  country: string;
}
