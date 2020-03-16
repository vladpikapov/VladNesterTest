import { Component, OnInit } from '@angular/core';
import {OrderedProduct, Orders, OrderService} from '../shared/OrderService/order.service';
import {Products, ProductService} from '../shared/product.service';
import {formatDate} from '@angular/common';

@Component({
  selector: 'app-add-order',
  templateUrl: './add-order.component.html',
  styleUrls: ['./add-order.component.css']
})
export class AddOrderComponent implements OnInit {
  products: Products[];
  order: Orders = new Orders();
  orderedProduct: OrderedProduct = new OrderedProduct();
  selectProductRowId;
  date = formatDate(new Date(), 'yyyy-MM-dd', 'en');
  countProduct = 1;

  constructor(private orderService: OrderService, private productService: ProductService) {
      this.productService.getProducts().subscribe(res => this.products = res as Products[]);
  }

  ngOnInit(): void {
  }

  SelectProduct(product: Products) {
    this.selectProductRowId = product.id;
  }

  checkCountProduct(event) {
    this.countProduct = event.target.value;
    console.log(this.date);
  }

  SetNullSelectProduct() {
    this.selectProductRowId = null;
  }

  addProductInOrder(product: Products) {
    this.orderedProduct.product = product;
    this.orderedProduct.countProduct = this.countProduct;
    this.order.orderedProducts.push(this.orderedProduct);
    console.log(this.order);
  }

  AddOrder() {
    console.log(this.order as Orders);
    // tslint:disable-next-line:max-line-length
    this.orderService.postOrder(this.order).subscribe(_ => this.productService.getProducts().subscribe(res => this.products = res as Products[]));
    this.order.orderedProducts = [];
  }
}
