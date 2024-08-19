import { Category } from "./category.interface";

export interface CardsResponse {
  status:    string;
  message:   null;
  data:      Card[];
  isSuccess: boolean;
}

export interface CardResponse {
  status:    string;
  message:   null;
  data:      Card;
  isSuccess: boolean;
}

export interface Card {
  id:                number;
  userId:            IdCard;
  userName:          string;
  phoneNumber?:      string;
  specialty:         string;
  experience:        string;
  description:       string;
  imageUrl:          string;
  profileCategories: any[];
  categories?:        Category[];
  categoriesString?:  string;
}

export type IdCard = number;
