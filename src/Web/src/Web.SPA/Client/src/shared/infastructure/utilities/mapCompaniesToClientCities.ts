import { Company } from '../models/backend/Company';
import { ClientCity } from '../models/common/ClientCity';

export const mapCompaniesToClientCities = (companies: Company[]): ClientCity[] => {
    if (!companies?.length) {
        throw new Error('List of companies is empty!');
    }

    const cities: ClientCity[] = [];

    companies.forEach(c => {
        if (!c.routes?.length) {
            throw new Error(`List of routes is empty for company ${c.caption.text}!`);
        }

        c.routes.forEach(r => {
            let sourceCities = cities.filter(city => city.id === r.start.id);
            let sourceCity = sourceCities[0];

            if (sourceCities.length > 1) {
                throw new Error(`List of cities contains multiple start city: ${sourceCities[0]?.caption.text}!`);
            } else if (!sourceCity) {
                sourceCity = {
                    id: r.start.id,
                    caption: r.start.caption,
                    routes: [r],
                }
                
                cities.push(sourceCity);
            } else {
                sourceCity.routes.push(r);
            }
        });
    });

    return cities;
};