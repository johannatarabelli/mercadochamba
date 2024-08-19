export interface ProfileResponse {
  status: string;
  message: null;
  data: Profile;
  isSuccess: boolean;
}

export interface Profile {
  id?: number;
  userId?: number;
  specialty: string;
  experience: string;
  description: string;
  image?: File | null;
  imageUrl: string;
  category?: number;
  categories?: Category[];
}

export interface Category {
  id: number;
  name: string;
}

