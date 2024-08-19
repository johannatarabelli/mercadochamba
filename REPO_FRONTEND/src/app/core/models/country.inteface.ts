import { Province } from "./province.interface";

export interface Country {
    id?: number;
    name: string;
    provinces?: null | Province[];
}