import { mergeMap as _observableMergeMap, catchError as _observableCatch } from 'rxjs/operators';
import { Observable, throwError as _observableThrow, of as _observableOf } from 'rxjs';
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { PageResultBase } from 'src/app/dto/page-result-base';
import { AreaDto, BaseSelectDto, StudentDto } from 'src/app/dto/output-dto';

@Injectable({ providedIn: 'root' })
export class StudentServiceProxy {
   
    private baseUrl: string;
    private headers!: HttpHeaders

    constructor(private http: HttpClient) {
        this.baseUrl = "https://localhost:44332";
        this.headers = new HttpHeaders({
            "authorization": "Bearer " + localStorage.getItem('access_token') ?? "",
        })
    }

    getList(keyWord: string | null | undefined, pageIndex: number, pageSize: number): Observable<PageResultBase<StudentDto>> {
        let url = this.baseUrl + `/api/student/get-list?Keyword=${keyWord}&PageIndex=${pageIndex}&PageSize=${pageSize}`;
        url = url.replace(/[?&]$/, "");
        return this.http.get<PageResultBase<StudentDto>>(url, { headers: this.headers, observe: 'body', responseType: 'json' });
    }

    updateContractPaid(contractId: any, data: any): Observable<any> {
        let url = this.baseUrl + `/api/student/update-contract-fee`;
        url = url.replace(/[?&]$/, "");

        const content = new FormData();
        if (contractId !== null && contractId !== undefined)
            content.append("contractId", contractId);
        if (data.datePaid !== null && data.datePaid !== undefined)
            content.append("datePaid", data.datePaid);
        if (data.moneyPaid !== null && data.moneyPaid !== undefined)
            content.append("moneyPaid", data.moneyPaid.toString());

        return this.http.put<any>(url, content, { headers: this.headers, observe: 'body', responseType: 'json' } );
    }
}
