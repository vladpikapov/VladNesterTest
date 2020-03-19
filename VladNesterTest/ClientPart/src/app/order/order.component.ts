import {Component, OnInit} from '@angular/core';
import * as XLSX from 'xlsx';
import {OrderService} from '../shared/OrderService/order.service';
import {OrderExport, Orders} from '../shared/Models/AllModels';

@Component({
  selector: 'app-order',
  templateUrl: './order.component.html',
  styleUrls: ['./order.component.css']
})
export class OrderComponent implements OnInit {
  Orders: Orders[] = [];
  ExportOrder: OrderExport;
  ExportOrders: OrderExport[] = [];
  EditOrderId;
  fileName = 'Orders.xlsx';


  constructor(private service: OrderService) {
  }

  ngOnInit(): void {
    this.service.GetOrders().subscribe(res => this.Orders = res as Orders[]);
    this.getOrderExport();

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
    this.getOrderExport();
    console.log(this.ExportOrders);
    const ws: XLSX.WorkSheet = XLSX.utils.json_to_sheet(this.ExportOrders);

    const wb: XLSX.WorkBook = XLSX.utils.book_new();
    XLSX.utils.book_append_sheet(wb, ws, 'Sheet1');
    XLSX.writeFile(wb, this.fileName);
  }

  getOrderExport() {
    // tslint:disable-next-line:prefer-for-of
    for (let i = 0; i < this.Orders.length; i++) {
      this.ExportOrder = new OrderExport();
      this.ExportOrder.id = this.Orders[i].id;
      this.ExportOrder.ordererName = this.Orders[i].ordererName;
      this.ExportOrder.orderStatus = this.Orders[i].orderStatus;
      this.ExportOrder.startDate = this.Orders[i].startDate;
      this.ExportOrder.endDate = this.Orders[i].endDate;
      // tslint:disable-next-line:prefer-for-of
      for (let j = 0; j < this.Orders[i].orderedProducts.length; j++) {
        if (j + 1 !== this.Orders[i].orderedProducts.length) {
          this.ExportOrder.idProduct += this.Orders[i].orderedProducts[j].product.id.toString() + ', ';
          this.ExportOrder.countProduct += this.Orders[i].orderedProducts[j].countProduct.toString() + ', ';
          this.ExportOrder.productName += this.Orders[i].orderedProducts[j].product.name + ',';
          this.ExportOrder.productType += this.Orders[i].orderedProducts[j].product.type + ',';
        } else {
          this.ExportOrder.idProduct += this.Orders[i].orderedProducts[j].product.id.toString();
          this.ExportOrder.countProduct += this.Orders[i].orderedProducts[j].countProduct.toString();
          this.ExportOrder.productName += this.Orders[i].orderedProducts[j].product.name;
          this.ExportOrder.productType += this.Orders[i].orderedProducts[j].product.type;
        }
      }
      this.ExportOrders.push(this.ExportOrder);
    }
  }

}


