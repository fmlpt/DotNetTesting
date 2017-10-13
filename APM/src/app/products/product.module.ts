import {NgModule} from '@angular/core';
import {ProductListComponent} from './product-list.component';
import {ProductDetailComponent} from './product-detail.component';
import {RouterModule} from '@angular/router';
//import { ProductGuardService } from './product-guard.service';
import {ProductService} from './product.service';
import {ProductFilterPipe} from './product-filter.pipe';
import {FormsModule} from '@angular/forms';
import {SharedModule} from './../shared/shared.module';

@NgModule({
  imports: [
    RouterModule.forChild([
      {path: 'products', component: ProductListComponent},
      {
        path: 'products/:id',
        //canActivate: [ ProductGuardService ],
        component: ProductDetailComponent
      }
    ]),
    SharedModule,
    FormsModule
  ],
  declarations: [
    ProductListComponent,
    ProductDetailComponent,
    ProductFilterPipe
  ],
  providers: [
    ProductService,
    //ProductGuardService
  ]
})
export class ProductModule {
}
