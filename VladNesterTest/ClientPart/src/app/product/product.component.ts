import { Component, OnInit } from '@angular/core';
import {HttpClient} from '@angular/common/http';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.css']
})
export class ProductComponent implements OnInit {

  Products: Product[];
  constructor(private http: HttpClient) {
    http.get('http://localhost:63170/api/Product').toPromise().then(res => this.Products = res as Product[]);
  }

  ngOnInit(): void {
  }

}
export interface Product {
  id: number;
  name: string;
}
