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
    contractCompletedStatus: number;
    isExtendContract: boolean;
    roomPrice: number;
    isDelete:boolean
    isSummerContract: boolean;
}

export interface ExtendContractTime {
    fromDate: Date;
    toDate: Date;
}

export interface AreaDto {
    id: number;
    name: string;
    totalRoom: number;
}

export interface BaseSelectDto {
    id: number;
    name: string;
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
    isPaid: boolean;
}

export interface FacilityDto {
    id: number;
    name: string
    totalCount: number;
    status: string;
}


export interface BillServiceDto {
    id: number;
    code: string
    roomId: number;
    fromDate: Date;
    toDate: Date;
    totalPrice: number;
    isPaid: boolean;
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

export interface RoomServiceDto {
    id: number;
    roomId: number;
    serviceId: number;
    serviceName: string;
    quantity:number;
    fromDate: Date;
    toDate: Date;
    statBegin: number;
    statEnd: number;
    totalServicePrice: number;
    paidDate: Date;
    moneyPaid: number;
    isPaid: boolean
}

export interface AddRoomServiceRequest {
    roomId: number;
    serviceId: number;
    fromDate: Date;
    toDate: Date;
    statBegin: number;
    statEnd: number;
}


export interface DisciplineDto {
    studentId: number
    name: string
    dob: Date;
    roomName: string;
    areaName: string;
    class: string
    studentCode: string
    phone: string
    major: string
    gender: number;
    disciplineId: number;
    description: string
    punish: string
}