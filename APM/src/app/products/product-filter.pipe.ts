import {PipeTransform, Pipe} from '@angular/core';
import {IProduct} from './product';

@Pipe({
  name: 'productFilter'
})
export class ProductFilterPipe implements PipeTransform {
  transform(value: IProduct[], term: string): IProduct[] {
    const filter: string = term ? term.toLocaleLowerCase() : null;

    return filter ? value.filter((product: IProduct) =>
      product.productName.toLocaleLowerCase().indexOf(filter) !== -1) : value;
  }

}

