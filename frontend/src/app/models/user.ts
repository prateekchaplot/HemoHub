export enum UserRole {
    Member,
    Volunteer,
    Admin
}

export interface User {
    name: string;
    email: string;
    role: UserRole;
}

export interface UserIdAndName {
    id: string;
    name: string;
}
