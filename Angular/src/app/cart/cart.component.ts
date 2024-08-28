import { Component } from '@angular/core';
import { NavbarComponent } from "../navbar/navbar.component";
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';

interface CartItem {
  productName: string;
  quantity: number;
  price: number;
}


@Component({
  selector: 'app-cart',
  standalone: true,
  imports: [NavbarComponent,ReactiveFormsModule],
  templateUrl: './cart.component.html',
  styleUrl: './cart.component.css'
})
export class CartComponent {
  cartForm: FormGroup;
  cartItems: CartItem[] = [];
  totalAmount: number = 0;

  constructor(private fb: FormBuilder) {
    this.cartForm = this.fb.group({
      productName: [''],
      price: [0],
      quantity: [1]
    });
  }

  addToCart() {
    if (this.cartForm.valid) {
      const newItem: CartItem = {
        productName: this.cartForm.get('productName')?.value,
        price: this.cartForm.get('price')?.value,
        quantity: this.cartForm.get('quantity')?.value
      };

      this.cartItems.push(newItem);
      this.updateTotalAmount();

      this.cartForm.reset({ productName: '', price: 0, quantity: 1 });
    } else {
      alert('Please fill in all required fields.');
    }
  }

  updateTotalAmount() {
    this.totalAmount = this.cartItems.reduce((total, item) => {
      return total + item.price * item.quantity;
    }, 0);
  }

  pay() {
    if (this.cartItems.length > 0) {
      alert(`Payment successful! Total amount paid: $${this.totalAmount}`);
      this.cartItems = [];
      this.totalAmount = 0;
    } else {
      alert('Your cart is empty.');
    }
  }
}
