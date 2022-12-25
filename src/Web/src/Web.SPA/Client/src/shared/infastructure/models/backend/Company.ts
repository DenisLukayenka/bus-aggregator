import { Caption } from '../common/Caption';
import { Route } from './Route';

export type Company = {
    id: string;
    caption: Caption;
    info: Caption;

    routes: Route[];
}