export interface pageResultBase<T> {
    pageIndex: number;
    pageSize: number;
    totalRecords: number;
    pageCount: number;
    items: T[];
}