import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class OrderService {

  baseUrl = document.getElementsByTagName('base')[0].href;

  constructor(private http: HttpClient) {
  }

  public GetOrders() {
    return this.http.get( this.baseUrl + 'api/Order');
  }

  public postOrder(form: any) {
    return this.http.post(this.baseUrl + 'api/Order', form);
  }

  public updateOrder(value: any) {
    return this.http.put(this.baseUrl + 'api/Order', value);
  }
}

