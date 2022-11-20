export type Position = {
    x: number;
    y: number;
}
export type CaptionPosition = 'top' | 'bottom' | 'right'; // Cannot use 'left' because we don't know the actual length of caption

export type Indicator = {
    position: Position;
    style?: string;
    size?: number;
}
export type Text = {
    style?: string;
    position: CaptionPosition | Position;
    size?: number;
}

export type City = {
    caption: string;

    text: Text;
    indicator: Indicator;
}

export type Path = {
    path: string;
    style?: string;
}

export interface Map {
    id: number;
    caption: string;
    description?: string;

    pathes: Path[];
    cities: City[];
}

export const DEFAULT_INDICATOR_SIZE = 24;
export const DEFAULT_TEXT_SIZE = 24;