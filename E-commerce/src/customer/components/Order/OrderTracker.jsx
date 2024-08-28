import * as React from 'react';
import { Box, Step, StepLabel, Stepper } from '@mui/material'

const steps=[
    "placed",
    "Order Confirmed",
    "Shipped",
    "Out For Delivery",
    "Delivered"
]

function OrderTracker({activeStep}) {
  return (
    <Box sx={{ width: '100%' }}>
        <Stepper activeStep={activeStep} alternativeLabel>
            {steps.map((label)=>(
          <Step key={label}>
            <StepLabel  sx={{ color: '#9155FD',fontSize: '44px' }}  className={``}>{label}</StepLabel>
          </Step>
        ))}
        </Stepper>

    </Box>
  )
}

export default OrderTracker