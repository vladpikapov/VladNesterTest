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
}

export interface Products {
  id: number;
  name: string;
  type: string;
  country: string;
}
