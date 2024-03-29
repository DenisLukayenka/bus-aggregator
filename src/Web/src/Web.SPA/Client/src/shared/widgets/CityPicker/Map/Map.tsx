import React, { useEffect, useState } from 'react';

import './Map.css';
import { useSelectedCities } from '@shared/hooks/useSelectedCities';
import { DEFAULT_INDICATOR_SIZE, DEFAULT_TEXT_SIZE, Indicator, MapInfo, Position, Text } from '@shared/infastructure/models/backend/Map';
import { AppConfig } from '@shared/infastructure/config';

type MapProps = {
    styles: {
        height: number | string,
        width: number | string,
    }
}

export const Map = ({ styles }: MapProps) => {
    const { selectedCities, setSelectedCity } = useSelectedCities();
    const [mapInfo, setMapInfo ] = useState<MapInfo | undefined>();

    useEffect(() => {
        fetch(AppConfig.mapEndpoint.mapUrl)
            .then(result => result.json())
            .then(data => setMapInfo(data));
    }, []);

    const onCityClick = (id: string) => {
        setSelectedCity(id);
    };

    const isCitySelected = (id: string) => selectedCities.includes(id);

    const getCityGroupClass = (id: string) =>
        `city-group ${isCitySelected(id) ? 'selected' : ''}`;

    const getIndicatorOverlayClass = (id: string) =>
        `indicator-overlay ${isCitySelected(id) ? 'visible' : ''}`;

    const getCaptionPosition = (indicator: Indicator, text: Text) => {
        if (!!text.position) {
            return { x: text.position.x, y: text.position.y };
        }

        const indicatorSize = indicator.size || DEFAULT_INDICATOR_SIZE;
        const textSize = text.size || DEFAULT_TEXT_SIZE;
        const position = indicator.position;

        switch(text.align) {
            case 'top':
                return { x: position.x - indicatorSize, y: position.y - indicatorSize / 2 - textSize / 2 };
            case 'bottom':
                return { x: position.x - indicatorSize, y: position.y + indicatorSize  + textSize / 2 };
            case 'right':
                return { x: position.x + (indicatorSize * 2 / 3), y: position.y + (textSize / 3)};
            default:
                throw new Error('Incorrect position value: ' + text.position);
        }
    }

    const getIndicatorRadiusData = (indicator: Indicator) => {
        const radius = !!indicator.size ? indicator.size / 2 : DEFAULT_INDICATOR_SIZE / 2;

        return { rx: radius, ry: radius };
    }

    const getTextSize = (text: Text) => {
        return text.size || DEFAULT_TEXT_SIZE;
    }

    if (!mapInfo) {
        return <div>Loading...</div>
    }

    return (
        <svg height={styles.height} width={styles.width} viewBox="0 0 500 500" xmlns="http://www.w3.org/2000/svg">
            {mapInfo.paths.map((v, i) => (
                <path key={i} style={v.style as any || {}} className='country-border' d={v.value}></path>
            ))}

            {mapInfo.cities.map(v => (
                <g id={v.text.caption} key={v.text.caption} className={getCityGroupClass(v.text.caption)} onClick={() => onCityClick(v.text.caption)}>
                    <ellipse
                        className={getIndicatorOverlayClass(v.text.caption)}
                        cx={v.indicator.position.x}
                        cy={v.indicator.position.y}
                        {...getIndicatorRadiusData(v.indicator)}
                    ></ellipse>
                    <ellipse
                        className='city-indicator'
                        cx={v.indicator.position.x}
                        cy={v.indicator.position.y}
                        {...getIndicatorRadiusData(v.indicator)}
                    ></ellipse>

                    <text
                        className='city-caption'
                        style={{fontSize: getTextSize(v.text)}}
                        {...getCaptionPosition(v.indicator, v.text)}
                    >{v.text.caption}</text>
                </g>
            ))}
        </svg>
    )
}