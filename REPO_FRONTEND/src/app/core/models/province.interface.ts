import { Neighborhood } from "./neighborhood.interface";

export interface Province {
    id?:            number;
    name:          string;
    neighborhoods?: Neighborhood[];
}
