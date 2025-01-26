import { User } from "./user.model";

export interface Department {
    id: number,
    name: string,
    isDeleted: boolean,
    users: User[];
}
