export interface JobResponse {
    status:    string;
    message:   null;
    data:      Job[];
    isSuccess: boolean;
}

export interface Job {
    id:          number;
    profileId:   number;
    userId:      number;
    title:       string;
    description: string;
    imageUrl:    string;
    createdAt:   Date;
    profile:     null;
    user:        null;
}
