import { Component, OnInit } from '@angular/core';
import {OrderedProduct, Orders, OrderService} from '../shared/OrderService/order.service';
import {Form, FormArray, FormBuilder, FormControl, FormControlName, Validators} from '@angular/forms';
import {Products, ProductService} from '../shared/product.service';

@Component({
  selector: 'app-order',
  templateUrl: './order.component.html',
  styleUrls: ['./order.component.css']
})
export class OrderComponent implements OnInit {
  addArray = [1];
  products: Products[];
  Orders: Orders[];
  addForm;

  constructor(private service: OrderService, private formBuilder: FormBuilder, private prodService: ProductService) {
    this.addForm = this.formBuilder.group({
      ordererName: new FormControl('', Validators.required),
      orderStatus: new FormControl('', Validators.required),
      startDate: new FormControl('', Validators.required),
      endDate: null,
      orderedProducts: this.formBuilder.group({
        product: this.formBuilder.group({
          id: '',
          name: '',
          type: '',
          country: '',
          count: ''
        }),
        countProduct: ''
      })
    });
  }
  ngOnInit(): void {
    this.service.GetOrders().subscribe(res => this.Orders = res as Orders[], error => console.log(error));
  }

  getProducts() {
    this.prodService.getProducts().subscribe(res => this.products = res as Products[]);
  }

  AddProduct() {
    this.addArray.push(null);
  }
  RemoveProduct() {
    this.addArray.pop();
  }


  AddOrder(value: any) {
    console.log(value);
  }
}

