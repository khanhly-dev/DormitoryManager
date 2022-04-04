export interface StudentInfoDto {
    id: number;
    name: string;
    dob: Date;
    baseAdress: string;
    adress: string;
    class: string;
    studentCode: string;
    phone: string;
    email: string;
    major: string;
    gender: number;
    academicYear: string;
    relativeName: string;
    relativePhone: string;
    ethnic: string;
    religion: string;
    point: number;
    userId: number;
    userName: string;
}

export interface CriteriaDto {
    value: number;
    label: string;
    checked: boolean;
}

export interface ContractPendingDto {
    id: number;
    contractCode: string;
    dateCreated: Date;
    desiredPrice: number;
    studentId: number;
    studentName: string;
    studentCode: string;
    studentPhone: string;
    adress: string;
    gender: number;
    point: number;
    adminConfirmStatus: number;
    studentConfirmStatus: number;
    roomId: number;
    roomName: number;
    areaName: number;
    academicYear: string
    toDate: Date;
    fromDate: Date;
}