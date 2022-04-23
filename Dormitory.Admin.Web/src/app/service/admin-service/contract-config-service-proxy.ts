import { mergeMap as _observableMergeMap, catchError as _observableCatch } from 'rxjs/operators';
import { Observable, throwError as _observableThrow, of as _observableOf } from 'rxjs';
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { PageResultBase } from 'src/app/dto/page-result-base';
import { ContractConfig, ContractConfigSelect } from 'src/app/dto/output-dto';

@Injectable({ providedIn: 'root' })
export class ContracConfigServiceProxy {
    private baseUrl: string;
    private headers!: HttpHeaders

    constructor(private http: HttpClient) {
        this.baseUrl = "https://localhost:44332";
        this.headers = new HttpHeaders({
            "authorization": "Bearer " + localStorage.getItem('access_token') ?? "",
        })
    }

    getList(keyWord: string | null | undefined, pageIndex: number, pageSize: number): Observable<PageResultBase<ContractConfig>> {
        let url = this.baseUrl + `/api/contract-time-config/get-list?Keyword=${keyWord}&PageIndex=${pageIndex}&PageSize=${pageSize}`;
        url = url.replace(/[?&]$/, "");
        return this.http.get<PageResultBase<ContractConfig>>(url, { headers: this.headers, observe: 'body', responseType: 'json' });
    }
    getListSelect(): Observable<ContractConfigSelect[]> {
        let url = this.baseUrl + `/api/contract-time-config/get-list-select`;
        url = url.replace(/[?&]$/, "");
        return this.http.get<ContractConfigSelect[]>(url, { headers: this.headers, observe: 'body', responseType: 'json' });
    }

    delete(id: number): Observable<any> {
        let url = this.baseUrl + `/api/contract-time-config/delete?id=${id}`;
        url = url.replace(/[?&]$/, "");

        return this.http.delete<any>(url, { headers: this.headers, observe: 'body', responseType: 'json' });
    }

    createOrUpdate(data: any): Observable<any> {
        let url = this.baseUrl + "/api/contract-time-config/create-or-update";
        url = url.replace(/[?&]$/, "");

        const content = new FormData();
        if (data.id !== null && data.id !== undefined)
            content.append("id", data.id);
        if (data.name !== null && data.name !== undefined)
            content.append("name", data.name.toString());
        if (data.fromDate !== null || data.fromDate !== undefined)
            content.append("fromDate", data.fromDate);
        if (data.toDate !== null || data.toDate !== undefined)
            content.append("toDate", data.toDate);
        if (data.isSummerSemester !== null || data.isSummerSemester !== undefined)
            content.append("isSummerSemester", data.isSummerSemester);

        return this.http.post<any>(url, content, { headers: this.headers, observe: 'body', responseType: 'json' } );
    }
}
