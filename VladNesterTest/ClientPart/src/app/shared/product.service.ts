import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Products} from './Models/AllModels';


@Injectable({
  providedIn: 'root'
})
export class ProductService {

  baseUrl = document.getElementsByTagName('base')[0].href;

  constructor(private http: HttpClient) {
  }

  public getProducts() {
    return this.http.get(this.baseUrl + 'api/Product');
  }

  public postProducts(product: Products) {
    return this.http.post(this.baseUrl + 'api/Product', product);
  }

  public addProductOneValue(id: number) {
    return this.http.put(this.baseUrl + 'api/Product/add/' + id, null);
  }

  public dropProductOneValue(id: number) {
    return this.http.put(this.baseUrl + 'api/Product/drop/' + id, null);
  }

}
