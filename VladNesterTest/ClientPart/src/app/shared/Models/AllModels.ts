export class Orders {
  id: number;
  ordererName: string;
  orderStatus: string;
  startDate: Date;
  endDate: Date;
  orderedProducts: OrderedProduct[] = [];
}

export class OrderedProduct {
  product: Products;
  countProduct: number;
}

export class Products {
  id: number;
  name: string;
  type: string;
  country: string;
  count: number;

  constructor() {
  }
}
export class OrderExport {
  id: number;
  ordererName: string;
  orderStatus: string;
  startDate: Date;
  endDate: Date;
  idProduct = '';
  productName = '';
  productType = '';
  countProduct = '';

  constructor() {
  }
}
