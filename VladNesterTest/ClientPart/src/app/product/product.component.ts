import { Component, OnInit } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Products, ProductService} from '../shared/product.service';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.css']
})
export class ProductComponent implements OnInit {

  Products: Products[];
  constructor(private service: ProductService) {
    service.getProducts().subscribe(res => this.Products = res as Products[], error => console.log(error));
  }

  ngOnInit(): void {
  }

}
