import { Route } from '../backend/Route';
import { Caption } from './Caption';

export type ClientCity = {
    id: string;
    caption: Caption;

    routes: Route[];
}