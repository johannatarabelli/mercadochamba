export interface Category {
  id: IdCategory;
  name: string;
  icon?: string;
  profileCategories?: null;
}

export interface CategoryResponse {
  status:    string;
  message:   null;
  data:      Category[];
  isSuccess: boolean;
}

export type IdCategory = number;
