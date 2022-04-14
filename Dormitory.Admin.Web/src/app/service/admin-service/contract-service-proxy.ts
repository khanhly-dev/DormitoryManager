import { mergeMap as _observableMergeMap, catchError as _observableCatch } from 'rxjs/operators';
import { Observable, throwError as _observableThrow, of as _observableOf } from 'rxjs';
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { PageResultBase } from 'src/app/dto/page-result-base';
import { ContractConfig, ContractDto, ContractFeeStatusDto, ContractPendingDto } from 'src/app/dto/output-dto';

@Injectable({ providedIn: 'root' })
export class ContracServiceProxy {
    private baseUrl: string;
    private headers!: HttpHeaders

    constructor(private http: HttpClient) {
        this.baseUrl = "https://localhost:44332";
        this.headers = new HttpHeaders({
            "authorization": "Bearer " + localStorage.getItem('access_token') ?? "",
        })
    }

    getList(keyWord: string | null | undefined, pageIndex: number, pageSize: number): Observable<PageResultBase<ContractDto>> {
        let url = this.baseUrl + `/api/contract/get-list-completed-contract?Keyword=${keyWord}&PageIndex=${pageIndex}&PageSize=${pageSize}`;
        url = url.replace(/[?&]$/, "");
        return this.http.get<PageResultBase<ContractDto>>(url, { headers: this.headers, observe: 'body', responseType: 'json' });
    }

    getListContractByStudentId(studentId : number): Observable<ContractFeeStatusDto[]> {
        let url = this.baseUrl + `/api/contract/get-list-contract-by-student?studentId=${studentId}`;
        url = url.replace(/[?&]$/, "");
        return this.http.get<ContractFeeStatusDto[]>(url, { headers: this.headers, observe: 'body', responseType: 'json' });
    }

    getListContractPending(keyWord: string | null | undefined, pageIndex: number, pageSize: number): Observable<PageResultBase<ContractPendingDto>> {
        let url = this.baseUrl + `/api/contract/get-list-contract-pending?Keyword=${keyWord}&PageIndex=${pageIndex}&PageSize=${pageSize}`;
        url = url.replace(/[?&]$/, "");
        return this.http.get<PageResultBase<ContractPendingDto>>(url, { headers: this.headers, observe: 'body', responseType: 'json' });
    }

    getListAdminConfirmContractPending(keyWord: string | null | undefined, pageIndex: number, pageSize: number): Observable<PageResultBase<ContractPendingDto>> {
        let url = this.baseUrl + `/api/contract/get-list-admin-confirm-contract-pending?Keyword=${keyWord}&PageIndex=${pageIndex}&PageSize=${pageSize}`;
        url = url.replace(/[?&]$/, "");
        return this.http.get<PageResultBase<ContractPendingDto>>(url, { headers: this.headers, observe: 'body', responseType: 'json' });
    }

    delete(id: number): Observable<any> {
        let url = this.baseUrl + `/api/contract/delete?id=${id}`;
        url = url.replace(/[?&]$/, "");

        return this.http.delete<any>(url, { headers: this.headers, observe: 'body', responseType: 'json' });
    }

    createOrUpdate(data: any): Observable<any> {
        let url = this.baseUrl + "/api/contract/create";
        url = url.replace(/[?&]$/, "");

        const content = new FormData();
        if (data.contractCode !== null && data.contractCode !== undefined)
            content.append("contractCode", data.contractCode.toString());
        if (data.fromDate !== null || data.fromDate !== undefined)
            content.append("fromDate", data.fromDate);
        if (data.toDate !== null || data.toDate !== undefined)
            content.append("toDate", data.toDate);
        if (data.roomId !== null || data.roomId !== undefined)
            content.append("roomId", data.roomId);
        if (data.studenId !== null || data.studenId !== undefined)
            content.append("studenId", data.studenId);
        if (data.serviceId !== null || data.serviceId !== undefined)
            content.append("serviceId", data.serviceId);
        if (data.adminConfirmStatus !== null || data.adminConfirmStatus !== undefined)
            content.append("adminConfirmStatus", data.adminConfirmStatus);
        if (data.studentConfirmStatus !== null || data.studentConfirmStatus !== undefined)
            content.append("studentConfirmStatus", data.studentConfirmStatus);

        return this.http.post<any>(url, content, { headers: this.headers, observe: 'body', responseType: 'json' } );
    }

    adminConfirmContract(contractId: any, confirmStatus: any): Observable<any> {
        let url = this.baseUrl + "/api/contract/admin-confirm";
        url = url.replace(/[?&]$/, "");

        const content = new FormData();
        if (contractId !== null && contractId !== undefined)
            content.append("contractId", contractId);
        if (confirmStatus !== null && confirmStatus !== undefined)
            content.append("confirmStatus", confirmStatus);

        return this.http.post<any>(url, content, { headers: this.headers, observe: 'body', responseType: 'json' } );
    }

    adminConfirmAllContract(): Observable<any> {
        let url = this.baseUrl + "/api/contract/admin-all-confirm";
        url = url.replace(/[?&]$/, "");

        const content = new FormData();

        return this.http.post<any>(url, content, { headers: this.headers, observe: 'body', responseType: 'json' } );
    }

    scheduleRoom(contractId: any): Observable<any> {
        let url = this.baseUrl + "/api/contract/schedule-room";
        url = url.replace(/[?&]$/, "");

        const content = new FormData();
        if (contractId !== null && contractId !== undefined)
            content.append("contractId", contractId);

        return this.http.put<any>(url, content, { headers: this.headers, observe: 'body', responseType: 'json' } );
    }

    autoScheduleRoom(listContractId: any): Observable<any> {
        let url = this.baseUrl + "/api/contract/auto-schedule-room";
        url = url.replace(/[?&]$/, "");

        const content = new FormData();
        if (listContractId !== null && listContractId !== undefined)
            content.append("listContractId", listContractId.toString());

        return this.http.put<any>(url, content, { headers: this.headers, observe: 'body', responseType: 'json' } );
    }

    changeRoom(contractId: any, roomId: any): Observable<any> {
        let url = this.baseUrl + "/api/contract/change-room";
        url = url.replace(/[?&]$/, "");

        const content = new FormData();
        if (contractId !== null && contractId !== undefined)
            content.append("contractId", contractId.toString());
        if (roomId !== null && roomId !== undefined)
            content.append("roomId", roomId.toString());

        return this.http.put<any>(url, content, { headers: this.headers, observe: 'body', responseType: 'json' } );
    }
}
