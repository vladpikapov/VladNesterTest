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
  customerId: number;
  customer: string;
  orderStatus: string;
  startDate: Date;
  endDate: Date;
  productName = '';
  productType = '';
  countProduct = '';

  constructor() {
  }
}
