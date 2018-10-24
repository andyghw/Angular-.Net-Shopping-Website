import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { NavMenuComponent } from './components/nav-menu/nav-menu.component';
import { HomeComponent } from './components/home/home.component';
import { FooterComponent } from './components/footer/footer.component';
import { HttpModule } from '@angular/http';
import { SearchProductComponent } from './components/search-product/search-product.component';
import { ProductService } from './services/product.service';
import { SearchFilterPipe } from './components/product-filter/product-filter.pipe';
import { DataService } from './services/data.service';
import { LoginComponent } from './components/login/login.component';
import { UserService } from './services/user.service';
import { ProductDetailComponent } from './components/product-detail/product-detail.component';
import { CartService } from './services/cart.service';
import { CartComponent } from './components/cart/cart.component';
import { PaymentComponent } from './components/payment/payment.component';
import { OrderService } from './services/order.service';
import { OrderComponent } from './components/order/order.component';
import { OrderDetailComponent } from './components/order-detail/order-detail.component';
import { AccountInfoComponent } from './components/account-info/account-info.component';
import { RegisterComponent } from './components/register/register.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    FooterComponent,
    SearchProductComponent,
    SearchFilterPipe,
    LoginComponent,
    ProductDetailComponent,
    CartComponent,
    PaymentComponent,
    OrderComponent,
    OrderDetailComponent,
    AccountInfoComponent,
    RegisterComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    HttpModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'search', component: SearchProductComponent },
      { path: 'login', component: LoginComponent },
      { path: 'product/:id', component: ProductDetailComponent },
      { path: 'cart', component: CartComponent },
      { path: 'payment', component: PaymentComponent },
      { path: 'order', component: OrderComponent },
      { path: 'order/:id', component: OrderDetailComponent },
      { path: 'user', component: AccountInfoComponent },
      { path: 'register', component: RegisterComponent },
    ])
  ],
  providers: [ProductService, DataService, UserService, CartService,OrderService],
  bootstrap: [AppComponent]
})
export class AppModule { }
