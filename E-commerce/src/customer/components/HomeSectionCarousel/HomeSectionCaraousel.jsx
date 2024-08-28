// import React from 'react'
// import AliceCarousel from 'react-alice-carousel'
// import HomeSectionCard from '../HomeSectionCard/HomeSectionCard';
// import { Button } from '@headlessui/react';
// import KeyboardArrowLeftIcon from '@mui/icons-material/KeyboardArrowLeft';

// const HomeSectionCaraousel = () => {
//     const responsive = {
//         0: { items: 1 },
//         720: { items: 3 },
//         1024: { items: 5.5 },
//     };

// const items=[1,1,1,1,1].map((item)=><HomeSectionCard/>)
//   return (
//     <div className="relative px-4 lg:px-8">
//         <div className="relative p-5">
//         <AliceCarousel
//         items={items}
//         disableButtonsControls
//         infinite
//         responsive={responsive}
//     />
//         <Button variant="contained" className="z-50" sx={{position:'absolute', top:"8rem", right:"0rem", transform:"translateX(50%) rotate(90deg)"}} aria-label="next">
//             <KeyboardArrowLeftIcon sx={{transform:"rotate(90deg)"}}/>
//         </Button>
//         </div>
        
//     </div>
//   )
// }

// export default HomeSectionCaraousel

// import React from 'react';
// import AliceCarousel from 'react-alice-carousel';
// import HomeSectionCard from '../HomeSectionCard/HomeSectionCard';
// import { Button } from '@headlessui/react';
// import KeyboardArrowLeftIcon from '@mui/icons-material/KeyboardArrowLeft';
// import { useState } from 'react';
// import { mens_kurta } from '../../../Data/mens_kurta';

// const HomeSectionCaraousel = () => {
//     const[activeIndex, setActiveIndex] = useState(0);
//     const responsive = {
//         0: { items: 1 },
//         720: { items: 3 },
//         1024: { items: 5.5 },
//     };

//     const slidePrev = () => setActiveIndex(activeIndex-1);
//     const slideNext = () => setActiveIndex(activeIndex+1);

//     const syncActiveIndex=({item})=>setActiveIndex(item)

//     const items = mens_kurta.slice(0,10).map((item) => <HomeSectionCard product={item}/>);

//     return (
//         <div className="border">
//             <div className="relative p-5 ">
//                 <AliceCarousel
//                     items={items}
//                     disableButtonsControls
//                     responsive={responsive}
//                     disableDotsControls
//                     onSlideChanged={syncActiveIndex}
//                     activeIndex={activeIndex}
//                 />
//                 {activeIndex !== items.length-5 && <Button
//                     variant="contained"
//                     className="z-50"
//                     onClick={slideNext}
//                     style={{
//                         position: 'absolute',
//                         top: '50%',
//                         right: '0',
//                         transform: 'translate(50%, -50%) rotate(90deg)',
//                         backgroundColor: 'white'
//                     }}
//                     aria-label="next"
//                 >
//                     <KeyboardArrowLeftIcon sx={{ transform: 'rotate(90deg)', color:"black" }} />
//                 </Button>}

//                 <Button
//                 onClick={slidePrev}
//                     variant="contained"
//                     className="z-50"
//                     style={{
//                         position: 'absolute',
//                         top: '50%',
//                         left: '0',
//                         transform: 'translate(50%, -50%) rotate(-90deg)',
//                         backgroundColor: 'white'
//                     }}
//                     aria-label="next"
//                 >
//                     <KeyboardArrowLeftIcon sx={{ transform: 'rotate(90deg)', color:"black" }} />
//                 </Button>
//             </div>
//         </div>
//     );
// }

// export default HomeSectionCaraousel;

import React, { useState, useRef } from 'react';
import AliceCarousel from 'react-alice-carousel';
import HomeSectionCard from '../HomeSectionCard/HomeSectionCard';
import { Button } from '@headlessui/react';
import ArrowForwardIosIcon from "@mui/icons-material/ArrowForwardIos";

const HomeSectionCaraousel = ({data,sectionName}) => {
    const [activeIndex, setActiveIndex] = useState(0);
    // const carouselRef = useRef(null);

 const slidePrev = () => setActiveIndex(activeIndex - 1);
  const slideNext = () => setActiveIndex(activeIndex + 1);
  const syncActiveIndex = ({ item }) => setActiveIndex(item);

  const responsive = {
    0: {
      items: 2,
      itemsFit: "contain",
    },
    568: {
      items: 3,
      itemsFit: "contain",
    },
    1024: {
      items: 5.5,
      itemsFit: "contain",
    },
  };
  const items = data?.slice(0, 10).map((item) => (
    <div className="">
      {" "}
      <HomeSectionCard product={item} />
    </div>
  ));

    return (
        <div className="relative px-4 sm:px-6 lg:px-8">
            <h2 className='text-2xl font-extrabold text-gray-800 py-5'>{sectionName}</h2>
            <div className="relative p-5">
                <AliceCarousel
                disableButtonsControls
                disableDotsControls
                mouseTracking
                items={items}
                activeIndex={activeIndex}
                responsive={responsive}
                onSlideChanged={syncActiveIndex}
                animationType='fadeout'
                animationDuration={2000}
     
                />
                {activeIndex !== items.length - 5 && (
          <Button
            onClick={slideNext}
            variant="contained"
            className="z-50 bg-[]"
            sx={{
              position: "absolute",
              top: "8rem",
              right: "0rem",
              transform: "translateX(50%) rotate(90deg)",
            }}
            color="white"
            aria-label="next"
          >
            <ArrowForwardIosIcon
              className=""
              sx={{ transform: "rotate(-90deg)" }}
            />
          </Button>
        )}

{activeIndex !== 0 && (
          <Button
            onClick={slidePrev}
            variant="contained"
            className="z-50 bg-[]"
            color="white"
            sx={{
              position: "absolute",
              top: "8rem",
              left: "0rem",
              transform: "translateX(-50%)  rotate(90deg)",
            }}
            aria-label="next"
          >
            <ArrowForwardIosIcon
              className=""
              sx={{ transform: " rotate(90deg)" }}
            />
          </Button>
        )}
            </div>
        </div>
    );
};

export default HomeSectionCaraousel;

