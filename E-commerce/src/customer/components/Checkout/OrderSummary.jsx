import React, { useEffect } from 'react'
import AddressCard from '../AddressCard/AddressCard'
import { Button } from '@mui/material'
import CartItem from '../Cart/CartItem'
import { useLocation, useNavigate } from 'react-router-dom'
import { useDispatch, useSelector } from 'react-redux'
import { getOrderById } from "../../../Redux/Customers/Order/Action";
import { createPayment } from "../../../Redux/Customers/Payment/Action";

const OrderSummary = () => {
const navigate = useNavigate();
const location = useLocation();
const searchParams = new URLSearchParams(location.search);
const orderId = searchParams.get("order_id");
const dispatch=useDispatch();
  const jwt=localStorage.getItem("jwt");
  const {order}=useSelector(state=>state)

console.log("orderId ", order.order)

useEffect(()=>{
  
    dispatch(getOrderById(orderId))
  },[orderId])
  
  const handleCreatePayment=()=>{
    const data={orderId:order.order?.id,jwt}
    dispatch(createPayment(data))
  }

  return (
    <div className="space-y-5">
        <div className='p-5 shadow-lg rounded-s-md border'>
            <AddressCard address={order.order?.shippingAddress}/>
        </div>

        <div> 
        <div className='lg:grid grid-cols-2  mt-10 relative'>
            <div className='cols-span-2 space-y-5 '>
            {order.order?.orderItems.map((item) => (
              <>
                <CartItem item={item} showButton={false}/>
              </>
            ))}
            </div> 
        </div> 
            <div className='px-5 sticky top-0 h-[100vh] mt-5 lg:mt-0 lg:w-[80%] w-[90%] ml-auto lg:mr-5 mr-3'>
            <div className='p-5 shadow-lg border rounded-md'>
                <p className='font-bold opacity-60 pb-4'>PRICE DETAILS</p>
                <hr/>
                <div className='space-y-3 font-semibold mb-5'>
                    <div className='flex justify-between pt-3 text-black'>
                        <span>Price ({order.order?.totalItem} item)</span>
                        <span>₹{order.order?.totalPrice}</span>
                    </div>

                    <div className='flex justify-between pt-3 '>
                        <span>Discount</span>
                        <span className='text-green-700'>-₹{order.order?.discounte} </span>
                    </div>

                    <div className='flex justify-between pt-3'>
                        <span>Delivery Charges</span>
                        <span className='text-green-700'>Free</span>
                    </div>
                    <hr/>
                    <div className='flex justify-between pt-3  font-bold'>
                        <span>Total Amount</span>
                        <span className='text-green-700'>₹{order.order?.totalDiscountedPrice}</span>
                    </div>
                </div>

                <Button
              onClick={handleCreatePayment}
              variant="contained"
              type="submit"
              sx={{ padding: ".8rem 2rem", marginTop: "2rem", width: "100%" }}
            >
              Payment
            </Button>
                
            </div>

        </div>
        
        </div>       
       
    </div>
  );
};

export default OrderSummary