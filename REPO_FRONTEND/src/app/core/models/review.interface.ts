export interface Review {
    name: string;
    date: string;
    stars: number;
    message: string;
    photoUrl: string;
  }
  
  // api-response.model.ts
  export interface Person {
    name: string;
    date: string;
    text: string;
    image: string;
  }
  
  export interface ApiResponse {
    status: string;
    code: number;
    total: number;
    data: Person[];
  }