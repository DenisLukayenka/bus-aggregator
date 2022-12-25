import { useEffect, useState } from 'react';

export const useSelectedCities = () => {
    const [selectedCity, setSelectedCity] = useState<string | undefined>();
    const [selectedCities, setSelectedCities] = useState<string[]>([]);

    useEffect(() => {
        if (!selectedCity) {
            return;
        }

        const cityIndex = selectedCities.indexOf(selectedCity);

        if (cityIndex > -1) {
            setSelectedCities(selectedCities.filter(c => c !== selectedCity));
        } else {
            if (selectedCities.length >= 2) {
                selectedCities.shift();
            }

            setSelectedCities([...selectedCities, selectedCity]);
        }

        setSelectedCity(undefined);
    }, [selectedCity]);

    return {selectedCities, setSelectedCity};
}