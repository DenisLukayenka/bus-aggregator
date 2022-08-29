import React from 'react';

import { CityPicker } from '@shared/widgets/CityPicker/CityPicker';
import { Header } from './Header/Header';
import './Main.css';

export const Main = () => {
    return (
        <div className='flex-column width-full'>
            <Header />

            <div className='content width-full flex-row'>
                <CityPicker />
            </div>
        </div>
    )
};