import { Component, OnInit } from '@angular/core';
import {OrderedProduct, Orders, OrderService} from '../shared/OrderService/order.service';
import {Form, FormArray, FormBuilder, FormControl, FormControlName, FormGroup, Validators} from '@angular/forms';
import * as XLSX from 'xlsx';

@Component({
  selector: 'app-order',
  templateUrl: './order.component.html',
  styleUrls: ['./order.component.css']
})
export class OrderComponent implements OnInit {
  Orders: Orders[];
  EditOrderId;
  fileName = 'Orders.csv';
  options = {
    fieldSeparator: ',',
    quoteStrings: '"',
    decimalseparator: '.',
    headers: ['1', '2', '3'],
    showTitle: true,
    useBom: true,
    removeNewLines: false,
    keys: []
  };

  constructor(private service: OrderService, private formBuilder: FormBuilder) {
  }

  ngOnInit(): void {
    this.service.GetOrders().subscribe(res => this.Orders = res as Orders[]);
  }

  changeStatus(order: Orders) {
    if (order.endDate == null) {
      this.EditOrderId = order.id;
    }
  }

  saveOrderStatus(order: Orders) {
    // tslint:disable-next-line:max-line-length
    this.service.updateOrder(order).subscribe(_ => this.service.GetOrders().subscribe(res => this.Orders = res as Orders[]));
    this.EditOrderId = null;
  }

  exportExcel(): void {

  }
}

