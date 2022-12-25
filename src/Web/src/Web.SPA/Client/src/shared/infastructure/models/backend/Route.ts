import { Company } from './Company';
import { City } from './City';

export type Route = {
    start: City;
    destination: City;

    company: Company;
}