import {Component, OnInit} from '@angular/core';
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
  productOr: Products = new Products();
  orderedProduct: OrderedProduct = new OrderedProduct();
  selectProductRowId = null;
  isAll = false;
  date = formatDate(new Date(), 'yyyy-MM-dd', 'en');
  countProduct = 1;

  constructor(private orderService: OrderService, private productService: ProductService) {
    this.productService.getProducts().subscribe(res => this.products = res as Products[]);
  }

  ngOnInit(): void {
  }

  SelectProduct(product: Products) {
    this.selectProductRowId = product.id;
    this.productOr = product;
  }

  checkCountProduct(event) {
    this.countProduct = event.target.value;
  }

  SetNullSelectProduct() {
    this.selectProductRowId = null;
  }

  addProductInOrder() {

    this.orderedProduct.product = this.productOr;
    this.orderedProduct.countProduct = this.countProduct;
    this.order.orderedProducts.push(this.orderedProduct);
    this.selectProductRowId = null;
    this.isAll = !this.isAll;
  }

  AddOrder() {
    // tslint:disable-next-line:max-line-length
    this.orderService.postOrder(this.order).subscribe(_ => this.productService.getProducts().subscribe(res => this.products = res as Products[]));
    this.order.orderedProducts = [];
    this.isAll = !this.isAll;
  }
}
