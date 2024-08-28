import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { NavbarComponent } from './navbar/navbar.component';
import { RegistrationComponent } from './registration/registration.component';
import { HttpClientModule } from '@angular/common/http';
import { BrowserModule } from '@angular/platform-browser';
import { CommonModule } from '@angular/common';
import { HomeComponent } from './home/home.component';
import { AdminComponent } from './admin/admin.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { UserloginComponent } from './userlogin/userlogin.component';
import { UserdashboardComponent } from './userdashboard/userdashboard.component';
import { ShipperComponent } from './shipper/shipper.component';
import { SellerComponent } from './seller/seller.component';
import { ProductComponent } from './product/product.component';
import { OrderComponent } from './order/order.component';
import { CategoryComponent } from './category/category.component';
import { SubcategoryComponent } from './subcategory/subcategory.component';

import { UserNavbarComponent } from './user-navbar/user-navbar.component';
import { ShopComponent } from './shop/shop.component';
import { CartComponent } from './cart/cart.component';


@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet,NavbarComponent,
    RegistrationComponent,
    HomeComponent,
    AdminComponent,
    DashboardComponent,
    UserloginComponent,
    UserNavbarComponent,
    UserdashboardComponent,
    ShipperComponent,
    SellerComponent,
    ProductComponent,
    OrderComponent,
    ShopComponent,
    CartComponent,
    CategoryComponent,
    SubcategoryComponent
 ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'SecondApp';
}
