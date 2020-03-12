import { Component, OnInit } from '@angular/core';
import {OrderedProduct, Orders, OrderService} from '../shared/OrderService/order.service';
import {Form, FormArray, FormBuilder, FormControl, FormControlName, Validators} from '@angular/forms';
import {Products} from '../shared/product.service';

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

  constructor(private service: OrderService, formBuilder: FormBuilder) {
    this.addForm = formBuilder.group({
      ordererName: new FormControl('', Validators.required),
      orderStatus: new FormControl('', Validators.required),
      startDate: new FormControl('', Validators.required),
      endDate: null,
      orderedProducts: formBuilder.group({
        product: formBuilder.group({
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

  AddProduct() {
    this.addArray.push(null);
  }
  RemoveProduct() {
    this.addArray.pop();
  }


}

