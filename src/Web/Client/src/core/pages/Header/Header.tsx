import React from 'react';
import './Header.css';

export const Header = () => {
    return (
        <header className='main-header flex-row width-full'>
            <div className='flex-row main-header-left'>
                <img src='favicon.ico'></img>
                <p className='company-name'>Future interprize</p>
            </div>
            <section className='flex-column'>
                <p className='contact-item'>+999 666 111</p>
                <p className='contact-item'>+999 666 111</p>
                <p className='contact-item'>+999 666 111</p>
            </section>
        </header>
    )
}