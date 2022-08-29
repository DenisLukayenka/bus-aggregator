import React from 'react';
import { Map } from '@shared/widgets/CityPicker/Map/Map';

export class CityPicker extends React.PureComponent {
    render() {
        return (
            <Map styles={{ height: '50%', width: '50%' }} />
        )
    }
}