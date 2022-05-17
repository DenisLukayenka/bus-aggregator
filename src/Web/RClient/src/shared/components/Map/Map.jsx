import React from 'react';
import './Map.css';
import { CountryData } from './Map.config.js';
import { useSelectedCities } from '../../hooks/useSelectedCities.js';

export const Map = (props) => {
    const cityTransform = 'matrix(0.17671, 0, 0, 0.18984, 74.863083, 210.296204)';
    const cityIndicatorSize = {
        rx: '8',
        ry: '8',
    };

    const {selectedCities, setSelectedCity} = useSelectedCities();

    const onCityClick = id => {
        setSelectedCity(id);
    };

    const isCitySelected = id => selectedCities.includes(id);

    const getCityGroupClass = id =>
        `city-group ${isCitySelected(id) ? 'selected' : undefined}`;

    const getIndicatorOverlayClass = id =>
        `indicator-overlay ${isCitySelected(id) ? 'visible' : undefined}`;

    return (
        <svg viewBox="0 0 500 500" xmlns="http://www.w3.org/2000/svg">
            <path className='country-border' d="M 191.098 400.307 C 191.098 402.923 197.498 403.963 199.341 405.942 C 201.463 408.224 200.964 413.322 203.089 415.603 C 208.902 421.85 219.925 413.045 227.067 415.603 C 229.76 416.567 231.694 437.2 238.309 430.094 C 242.261 425.849 247.952 411.123 259.291 417.214 C 262.621 419.003 263.344 422.959 267.534 424.459 C 272.839 426.359 277.348 422.928 281.024 426.875 C 281.565 427.456 288.768 438.681 291.514 435.73 C 294.45 432.577 293.856 424.359 296.76 421.238 C 298.634 419.227 305.379 422.446 307.253 420.435 C 313.246 413.995 318.17 446.544 328.234 435.73 C 329.02 434.887 327.732 431.172 328.234 430.094 C 329.475 427.43 339.51 436.114 343.223 434.119 C 345.235 433.04 358.214 426.074 362.706 430.899 C 364.062 432.355 379.63 450.556 383.688 446.194 C 387.561 442.037 379.817 425.399 383.688 421.238 C 391.528 412.815 389.718 395.404 394.93 384.205 C 399.801 373.736 411.28 385.888 418.91 381.791 C 420.06 381.173 422.634 377.754 425.655 379.375 C 428.489 380.899 440.112 386.385 444.388 381.791 C 447.235 378.732 442.891 375.234 442.891 372.129 C 442.891 360.254 441.069 357.751 436.896 348.784 C 432.289 338.89 442.352 322.441 435.396 314.97 C 431.584 310.876 423.968 309.133 420.409 305.31 C 418.344 303.091 419.534 288.538 421.158 286.793 C 422.601 285.242 438.703 284.543 441.391 285.987 C 444.628 287.727 446.742 290.931 448.886 293.233 C 449.97 294.398 457.735 293.233 459.377 293.233 C 466.334 293.233 480.369 284.353 483.358 277.937 C 485.544 273.237 498.054 272.484 493.848 263.445 C 487.78 250.405 478.703 236.71 470.617 228.022 C 464.576 221.532 452.566 239.223 447.387 233.659 C 441.508 227.342 451.973 211.214 445.888 204.676 C 442.589 201.132 433.077 204.637 428.653 202.26 C 424 199.761 424.898 190.98 421.158 186.964 C 418.478 184.085 406.171 174.492 406.171 170.863 C 410.044 170.863 418.078 161.919 413.664 157.177 C 407.97 151.06 392.681 156.197 392.681 145.906 C 392.681 138.156 395.354 133.374 399.426 129 C 404.731 123.3 395.28 111.235 394.181 108.873 C 393.129 106.614 398.001 107.183 398.677 106.456 C 400.055 104.977 398.55 91.696 397.926 90.355 C 394.939 83.937 388.759 71.651 383.688 66.204 C 379.697 61.916 371.557 66.129 365.704 62.984 C 358.732 59.238 352.723 53.453 343.971 58.153 C 341.935 59.247 327.977 72.368 325.237 69.425 C 319.283 63.028 322.043 47.476 316.244 41.246 C 304.478 28.605 287.768 66.274 287.768 38.831 C 279.276 38.831 255.044 43.717 255.044 31.027 C 245.151 36.342 240.365 41.161 234.812 47.128 C 231.713 50.458 234.737 60.089 231.064 64.035 C 227.146 68.244 214.279 63.391 213.08 60.815 C 212.764 60.139 203.532 60.815 202.588 60.815 C 197.862 60.815 183.874 74.456 182.355 77.72 C 179.821 83.165 175.696 97.135 180.106 101.873 C 182.962 104.941 185.837 102.938 189.849 105.094 C 191.697 106.086 194.148 114.16 192.098 116.365 C 186.623 122.245 162.052 110.879 157.626 120.39 C 154.53 127.039 143.325 145.782 138.141 148.567 C 137.154 149.097 136.205 159.296 135.893 160.643 C 134.649 165.99 127.417 183.739 133.645 190.431 C 134.495 191.343 143.809 198.028 140.39 201.701 C 130.824 211.98 126.319 189.132 119.408 192.846 C 108.946 198.464 92.404 198.898 82.687 204.116 C 80.339 205.378 82.802 212.043 81.188 213.777 C 77.411 217.834 70.612 218.652 64.702 221.828 C 54.466 227.325 42.085 216.997 34.726 216.997 C 32.783 216.997 23.977 215.664 22.736 216.997 C 21.214 218.634 23.737 224.785 24.236 225.854 C 26.279 230.245 32.059 240.698 35.474 244.37 C 36.692 245.678 35.474 255.198 35.474 257.25 C 35.474 262.314 37.409 270.599 39.972 273.352 C 41.015 274.472 46.716 325.855 46.716 295.088 C 45.717 305.699 47.316 321.304 41.222 327.851 C 37.414 331.94 28.753 330.524 23.236 333.486 C 17.213 336.722 13.145 350.766 8.998 355.223 C 7.858 356.449 9.467 363.778 9.747 364.079 C 11.994 366.494 17.99 364.884 20.239 367.3 C 23.514 370.82 27.031 381.092 29.231 385.816 C 36.17 400.725 21.171 415.998 26.983 428.486 C 27.983 430.633 31.398 426.961 32.979 425.265 C 33.977 424.192 31.978 419.897 32.979 418.824 C 37.566 413.896 53.653 423.985 59.206 418.018 C 65.037 411.754 64.079 399.097 69.698 393.062 C 73.731 388.727 89.881 397.087 95.176 397.087 C 119.849 397.087 148.635 382.868 165.619 401.113 C 170.888 406.773 188.199 395.635 191.098 400.307 Z"></path>

            {CountryData.map(i => (
                <g id={i.id} key={i.id} className={getCityGroupClass(i.id)} onClick={() => onCityClick(i.id)}>
                    <text className='city-caption' transform={cityTransform} {...i.captionPosition}>{i.caption}</text>

                    <ellipse className={getIndicatorOverlayClass(i.id)} {...i.indicatorPosition}></ellipse>
                    <ellipse className='city-indicator' {...i.indicatorPosition} {...cityIndicatorSize}></ellipse>
                </g>
            ))}
        </svg>
    )
}