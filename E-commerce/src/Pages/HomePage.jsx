import React from 'react'
import MainCarousel from '../customer/components/HomeCarousel/MainCarousel.jsx'
import HomeSectionCaraousel from '../customer/components/HomeSectionCarousel/HomeSectionCaraousel.jsx';
import { mens_kurta } from '../Data/Men/mens_kurta.js';
import { mensShoesPage1 } from '../Data/Shoes/shoes.js';
import { lengha_page1 } from '../Data/Women/LenghaCholi.js';
import { sareePage1 } from '../Data/Saree/page1.js';
import { dressPage1 } from '../Data/Dress/page1.js';
import { gounsPage1 } from '../Data/Gouns/gouns.js';
import { kurtaPage1 } from '../Data/Kurta/kurta.js';
import { mensPantsPage1 } from '../Data/Pants/men_page1.js';
import { mainCarouselData } from '../customer/components/HomeCarousel/MainCarouselData.js';

export const HomePage = () => {
  return (
    <div>
        <MainCarousel images={mainCarouselData}/>

        <div className='space-y-10 py-20 flex flex-col justify-center px-5 lg:px-10'>
            <HomeSectionCaraousel data={mens_kurta} sectionName={"Men's Kurta"}/>
            <HomeSectionCaraousel data={mensShoesPage1} sectionName={"Men's Shoes"}/>
            <HomeSectionCaraousel data={mensPantsPage1} sectionName={"Men's Pants"}/>
            {/* <HomeSectionCaraousel data={lengha_page1} sectionName={"Lengha Choli"}/> */}
            <HomeSectionCaraousel data={sareePage1} sectionName={"Saree"}/>
            <HomeSectionCaraousel data={dressPage1} sectionName={"Dress"}/>
            <HomeSectionCaraousel data={gounsPage1} sectionName={"Women's Gouns"} />
            <HomeSectionCaraousel data={kurtaPage1} sectionName={"Women's Kurtas"} />
        </div>
    </div>
  )
}

export default HomePage;
