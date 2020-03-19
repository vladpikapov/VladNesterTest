import {Component, OnInit} from '@angular/core';
import {formatDate} from '@angular/common';
import { Products, Orders, OrderedProduct } from '../shared/Models/AllModels';
import { OrderService } from '../shared/OrderService/order.service';
import { ProductService } from '../shared/product.service';

@Component({
  selector: 'app-add-order',
  templateUrl: './add-order.component.html',
  styleUrls: ['./add-order.component.css']
})
export class AddOrderComponent implements OnInit {
  products: Products[];
  order: Orders = new Orders();
  productOr: Products = new Products();
  orderedProduct: OrderedProduct = new OrderedProduct();
  selectProductRowId = null;
  thisOrderedProducts: OrderedProduct[] = [];
  isAll = false;
  date = formatDate(new Date(), 'yyyy-MM-dd', 'en');
  countProduct = 1;

  constructor(private orderService: OrderService, private productService: ProductService) {
    this.productService.getProducts().subscribe(res => this.products = res as Products[]);
  }

  ngOnInit(): void {
  }

  selectProduct(product: Products) {
    if (this.order.orderedProducts.length === 0) {
      this.selectProductRowId = product.id;
      this.productOr = product;
    }

  }

  checkCountProduct(event) {
    this.countProduct = event.target.value;
  }

  setNullSelectProduct() {
    this.selectProductRowId = null;
  }

  addProductInOrder() {

    this.orderedProduct.product = this.productOr;
    this.orderedProduct.countProduct = this.countProduct;
    this.order.orderedProducts.push(this.orderedProduct);
    this.selectProductRowId = null;
    this.isAll = !this.isAll;
  }

  addOrder() {
    // tslint:disable-next-line:max-line-length
    this.orderService.postOrder(this.order).subscribe(_ => this.productService.getProducts().subscribe(res => this.products = res as Products[]));
    this.thisOrderedProducts.push(this.order.orderedProducts.pop());
    this.orderedProduct = new OrderedProduct();
    this.isAll = !this.isAll;
  }

  removeProductFromOrder() {
    this.order.orderedProducts.pop();
  }
}
