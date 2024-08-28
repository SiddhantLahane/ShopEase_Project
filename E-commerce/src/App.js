// import logo from './logo.svg';
import './App.css';
// import Navigation from './customer/components/Navigation/Navigation';
// import HomePage from './customer/pages/HomePage/HomePage';
// import Footer from './customer/components/Footer/Footer';
// import Product from './customer/components/Product/Product';
// import ProductDetails from './customer/components/ProductDetails/ProductDetails';
// import Cart from './customer/components/Cart/Cart';
// import Checkout from './customer/components/Checkout/Checkout';
// import Order from './customer/components/Order/Order';
// import OrderDetails from './customer/components/Order/OrderDetails';
import { Route, Routes } from 'react-router-dom';
import CustomerRoutes from './Routers/CustomerRoutes';
import { useDispatch, useSelector } from 'react-redux';
import { useEffect } from 'react';
import { getUser } from './Redux/Auth/Action';
import AdminPannel from "./Admin/AdminPannel";

function App() {
  const {auth}=useSelector(store=>store);
  const dispatch = useDispatch();
  const jwt = localStorage.getItem("jwt");

  useEffect(() => {
    if (jwt) {
      dispatch(getUser(jwt));
    }
  }, [jwt]);
  return (
    <div className="">
      <Routes>
        <Route path="/*" element={<CustomerRoutes />} />
       {auth.user?.role==="ROLE_ADMIN" && <Route path="/admin/*" element={<AdminPannel />} />}
      </Routes>
    </div>
  );
}

export default App;
