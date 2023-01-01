export type Position = {
    x: number;
    y: number;
}

export type Indicator = {
    position: Position;
    style?: string;
    size?: number;
}
export type Text = {
    caption: string;
    style?: string;
    position?: Position;
    align?: 'top' | 'bottom' | 'right'; // Cannot use 'left' because we don't know the actual length of caption
    size?: number;
}

export type City = {
    text: Text;
    indicator: Indicator;
}

export type Path = {
    value: string;
    style?: string;
}

export interface MapInfo {
    id: number;
    caption: string;
    description?: string;

    paths: Path[];
    cities: City[];
}

export const DEFAULT_INDICATOR_SIZE = 24;
export const DEFAULT_TEXT_SIZE = 24;