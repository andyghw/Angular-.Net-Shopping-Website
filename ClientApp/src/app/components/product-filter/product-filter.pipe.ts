import { Pipe, PipeTransform } from '@angular/core';


@Pipe({
  name: 'productFilter'
})
export class SearchFilterPipe implements PipeTransform {//using pipe to filter items

  transform(products: any, search: any): any {
    if (search === undefined) {
      return products;          //if the result is null,all items will be returned
    } else {
      return products.filter((product) => {
        return product.name.toLowerCase().includes(search.toLowerCase());
      });
    }
  }

}
