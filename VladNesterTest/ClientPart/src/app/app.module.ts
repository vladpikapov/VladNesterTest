import {BrowserModule} from '@angular/platform-browser';
import {NgModule} from '@angular/core';
import {HttpClientModule} from '@angular/common/http';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {RouterModule, Routes} from '@angular/router';
import {Angular2CsvModule} from 'angular2-csv';

import {AppComponent} from './app.component';
import {OrderComponent} from './order/order.component';
import {ProductComponent} from './product/product.component';
import {AddOrderComponent} from './add-order/add-order.component';


const appRoutes: Routes = [
  {path: 'product_list', component: ProductComponent},
  {path: 'order_list', component: OrderComponent},
  {path: 'add-order', component: AddOrderComponent},
];

@NgModule({
  declarations: [
    AppComponent,
    OrderComponent,
    ProductComponent,
    AddOrderComponent,
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot(appRoutes, {enableTracing: true}),
    ReactiveFormsModule,
    Angular2CsvModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule {
}
