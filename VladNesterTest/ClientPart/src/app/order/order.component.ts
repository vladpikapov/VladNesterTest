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
  fileName = 'Orders.xlsx';

  constructor(private service: OrderService, private formBuilder: FormBuilder) {
  }

  ngOnInit(): void {
    this.service.GetOrders().subscribe(res => this.Orders = res as Orders[], error => console.log(error));
  }

  changeStatus(order: Orders) {
    if (order.endDate == null) {
      this.EditOrderId = order.id;
    }
  }

  saveOrderStatus(order: Orders) {
    console.log(order.orderStatus);
    // tslint:disable-next-line:max-line-length
    this.service.updateOrder(order).subscribe(_ => this.service.GetOrders().subscribe(res => this.Orders = res as Orders[]));
    this.EditOrderId = null;
  }

  exportExcel(): void {
    const ws: XLSX.WorkSheet = XLSX.utils.json_to_sheet(this.Orders);
    const wb: XLSX.WorkBook = XLSX.utils.book_new();
    XLSX.utils.book_append_sheet(wb, ws, 'Sheet1');
    XLSX.writeFile(wb, this.fileName);
    console.log(this.Orders);
  }
}

