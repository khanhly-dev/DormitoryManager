export interface AreaDto {
    id: number;
    name: string;
    totalRoom: number;
}

export interface BaseSelectDto {
    id: number;
    name: string;
    totalRoom: number;
}

export interface RoomDto {
    id: number;
    name: string
    price: number;
    areaId: number;
    areaName: string;
    maxSlot: number;
    minSlot: number;
    emptySlot: number;
    filledSlot: number;
    avaiableSlot: number;
}

export interface FacilityDto {
    id: number;
    name: string
    totalCount: number;
    status: string;
}

export interface ServiceDto {
    id: number;
    name: string
    price: number;
    unit: string;
    serviceType: number;
}

export interface CriteriaDto {
    id: number;
    name: string
    point: number;
}

export interface ContractConfig {
    id: number;
    name: string
    fromDate: Date;
    toDate: Date
}

export interface ContractDto {
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
    academicYear: string;
    fromDate: Date;
    toDate: Date;
    contractCompletedStatus: number;
    roomPrice: number;
    isExtendContract: number;
}
export interface ContractFeeStatusDto {
    id: number;
    contractCode: string;
    dateCreated: Date;
    studentId: number;
    roomId: number;
    roomName: number;
    areaName: number;
    fromDate: Date;
    toDate: Date;
    roomPrice: number;
    isExtendContract: number;
    contractPriceValue: number;
    servicePrice: number
    paidDate: Date
    moneyPaid: number
    isPaid: boolean
    contractPrice: number
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
}

export interface RoomSelectDto {
    id: number;
    name: string;
    avaiableSlot: number;
    genderRoom: number;
}

export interface StudentDto {
    id: number
    name: string
    dob: Date
    baseAdress: string;
    adress: string
    class: string
    studentCode: string
    phone: string
    email: string
    major: string
    gender: number
    academicYear: number;
    relativeName: string
    relativePhone: string
    ethnic: string
    religion: string
    point: number;
    paymentStatus: boolean
}
