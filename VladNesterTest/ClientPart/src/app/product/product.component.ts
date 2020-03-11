import { Component, OnInit } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Products, ProductService} from '../shared/product.service';
import {FormBuilder} from '@angular/forms';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.css']
})
export class ProductComponent implements OnInit {

  addForm;
  Products: Products[];
  constructor(private service: ProductService, private formBuilder: FormBuilder) {
    this.addForm = this.formBuilder.group({
      name: '',
      type: '',
      country: ''
    });
  }

  ngOnInit(): void {
    this.service.getProducts().subscribe(res => this.Products = res as Products[], error => console.log(error));
  }

  AddProduct(value: any) {
    // tslint:disable-next-line:max-line-length
    this.service.postProducts(value).subscribe(_ => this.service.getProducts().subscribe(res => this.Products = res as Products[]), error => console.log(error));
    this.addForm._reset();
    this.addForm.name = '';
    this.addForm.type = '';
    this.addForm.country = '';

  }

  DeleteProduct(id: number) {
    // tslint:disable-next-line:max-line-length
    this.service.deleteProducts(id).subscribe(_ => this.service.getProducts().subscribe(res => this.Products = res as Products[]), error => console.log(error));
  }
}
