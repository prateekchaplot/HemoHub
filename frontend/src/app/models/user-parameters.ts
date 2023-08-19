export interface requestParamters {
    pageNumber: number;
    pageSize: number;
}

export class UserParameters implements requestParamters {
    pageNumber: number;
    pageSize: number;
    bloodGroup: string;

    constructor(bloodGroup: string, pageIndex: number, pageSize: number) {
        this.bloodGroup = bloodGroup;
        this.pageNumber = pageIndex;
        this.pageSize = pageSize;
    }
}