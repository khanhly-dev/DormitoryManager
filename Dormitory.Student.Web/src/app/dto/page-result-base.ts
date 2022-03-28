export interface PageResultBase<T> {
    pageIndex: number;
    pageSize: number;
    totalRecords: number;
    pageCount: number;
    items: T[];
}