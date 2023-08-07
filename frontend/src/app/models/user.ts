export enum UserRole {
    Member,
    Volunteer,
    Admin
}

export interface User {
    nameid: string;
    email: string;
    role: UserRole;
}
