import { Routes } from '@angular/router';
import { NavbarComponent } from './navbar/navbar.component';
import { RegistrationComponent } from './registration/registration.component';
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
import { EditShipperComponent } from './edit-shipper/edit-shipper.component';
import { DetailsShipperComponent } from './details-shipper/details-shipper.component';

import { UserNavbarComponent } from './user-navbar/user-navbar.component';
import { ShopComponent } from './shop/shop.component';
import { CartComponent } from './cart/cart.component';



export const routes: Routes = [
  { path: 'navbar', component: NavbarComponent },
  { path: 'registration', component: RegistrationComponent },
  { path: '', component: HomeComponent },
  { path: 'admin', component: AdminComponent },
  { path: 'dashboard', component: DashboardComponent },
  { path: 'userlogin', component: UserloginComponent },
  { path: 'userdashboard', component: UserdashboardComponent },
  { path: 'shipper', component: ShipperComponent },
  { path: 'edit-shipper/:id', component: EditShipperComponent},
  { path: 'details-shipper/:id', component: DetailsShipperComponent },
  { path: 'seller', component: SellerComponent },
  { path: 'product', component: ProductComponent },
  { path: 'cart', component: CartComponent },
  { path: 'order', component: OrderComponent },
  { path: 'shop', component:ShopComponent },

  { path: 'category', component: CategoryComponent },
  { path: 'subcategory', component: SubcategoryComponent },
  { path: 'user-navbar', component: UserNavbarComponent }
];
