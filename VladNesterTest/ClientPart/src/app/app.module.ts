import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import {HttpClientModule} from '@angular/common/http';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {RouterModule, Routes} from '@angular/router';

import { AppComponent } from './app.component';
import { OrderComponent } from './order/order.component';
import { ProductComponent } from './product/product.component';

const appRoutes: Routes = [
  {path: 'product_list', component: ProductComponent},
  {path: 'order_list', component: OrderComponent},
  ];

@NgModule({
  declarations: [
    AppComponent,
    OrderComponent,
    ProductComponent,
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot(appRoutes, {enableTracing: true}),
    ReactiveFormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
