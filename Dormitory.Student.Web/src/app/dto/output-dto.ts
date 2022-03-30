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

