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
}

export interface CriteriaDto {
    id: number;
    name: string
    point: number;
}

export interface ContractConfig {
    id: number;
    name: string
    monthConfig: number;
}

export interface ContractDto {
    id: number;
    contractCode: string
    fromDate: Date;
    toDate: Date;
    roomId: number;
    desiredRoomId: number;
    studentId: number;
    serviceId: number;
    adminConfirmStatus: number;
    studentConfirmStatus: number;
}

export interface ContractPendingDto {
    id: number;
    contractCode: string;
    dateCreated: Date;
    desiredRoomId: number;
    desiredRoomName: string;
    studentId: number;
    studentName: string;
    studentCode: string;
    studentPhone: string;
    adress: string;
    gender: number;
    point: number;
    adminConfirmStatus: number;
}