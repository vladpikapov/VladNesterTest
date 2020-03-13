import { Component, OnInit } from '@angular/core';
import {OrderedProduct, Orders, OrderService} from '../shared/OrderService/order.service';
import {Form, FormArray, FormBuilder, FormControl, FormControlName, FormGroup, Validators} from '@angular/forms';
import {Products, ProductService} from '../shared/product.service';

@Component({
  selector: 'app-order',
  templateUrl: './order.component.html',
  styleUrls: ['./order.component.css']
})
export class OrderComponent implements OnInit {
  products: Products[];
  Orders: Orders[];
  orderStatus ;
  EditOrderId;
  addForm;

  constructor(private service: OrderService, private formBuilder: FormBuilder, private prodService: ProductService) {
    this.addForm = this.formBuilder.group({
      ordererName: '',
      orderStatus: '',
      startDate: '',
      endDate: null,
      orderedProducts: this.formBuilder.array([this.createOrderedProduct()], Validators.required)
    });
  }

  ngOnInit(): void {
    this.service.GetOrders().subscribe(res => this.Orders = res as Orders[], error => console.log(error));
  }

  getProducts() {
    this.prodService.getProducts().subscribe(res => this.products = res as Products[]);
  }

  createOrderedProduct(): FormGroup {
    return this.formBuilder.group({
      product: this.formBuilder.group({
        id: '',
        name: '',
        type: '',
        country: '',
        count: 1
      }),
      countProduct: 0
    });
  }

  addOrder(value: any) {

    console.log(JSON.stringify(value));
    this.service.postOrder(value).subscribe(_ => this.service.GetOrders().subscribe(res => this.Orders = res as Orders[]));
  }

  addToProductList() {
    this.addForm.get('orderedProducts').push(this.createOrderedProduct());
  }

  removeFromProductList(id: number) {
    this.addForm.get('orderedProducts').removeAt(id);
  }

  getMaxProductValue() {

  }

  parseToInt(id: number): number {
    return Number(id);
  }

  changeStatus(id: number) {
    this.EditOrderId = id;
  }

  saveOrderStatus(order: Orders) {
    console.log(order.orderStatus);
    // tslint:disable-next-line:max-line-length
    this.service.updateOrder(order.id, order).subscribe();
    this.EditOrderId = null;
  }
}

