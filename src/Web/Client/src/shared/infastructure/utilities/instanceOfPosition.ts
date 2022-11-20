import { CaptionPosition, Position } from "../models/backend/Map";

export const instanceOfPosition = (position: Position | CaptionPosition): position is Position => {
    if (typeof position === 'object') {
        return 'x' in position && 'y' in position;
    }

    return false;
}
