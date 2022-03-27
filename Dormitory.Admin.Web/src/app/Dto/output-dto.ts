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