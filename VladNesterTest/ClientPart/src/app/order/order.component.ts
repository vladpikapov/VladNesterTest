import { Component, OnInit } from '@angular/core';
import {Orders, OrderService} from '../shared/OrderService/order.service';

@Component({
  selector: 'app-order',
  templateUrl: './order.component.html',
  styleUrls: ['./order.component.css']
})
export class OrderComponent implements OnInit {
  Orders: Orders[];

  constructor(private service: OrderService) {
    service.GetOrders().subscribe(res => this.Orders = res as Orders[], error => console.log(error));
  }

  ngOnInit(): void {
  }
}

