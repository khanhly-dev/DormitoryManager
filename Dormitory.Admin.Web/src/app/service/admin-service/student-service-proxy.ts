import { mergeMap as _observableMergeMap, catchError as _observableCatch } from 'rxjs/operators';
import { Observable, throwError as _observableThrow, of as _observableOf } from 'rxjs';
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { PageResultBase } from 'src/app/dto/page-result-base';
import { BaseSelectDto, DisciplineDto, StudentDto } from 'src/app/dto/output-dto';

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

    getAll(keyWord: string | null | undefined, pageIndex: number, pageSize: number): Observable<PageResultBase<StudentDto>> {
        let url = this.baseUrl + `/api/student/get-all?Keyword=${keyWord}&PageIndex=${pageIndex}&PageSize=${pageSize}`;
        url = url.replace(/[?&]$/, "");
        return this.http.get<PageResultBase<StudentDto>>(url, { headers: this.headers, observe: 'body', responseType: 'json' });
    }

    getListStudentSelect(): Observable<BaseSelectDto[]> {
        let url = this.baseUrl + `/api/student/get-list-student-select`;
        url = url.replace(/[?&]$/, "");
        return this.http.get<BaseSelectDto[]>(url, { headers: this.headers, observe: 'body', responseType: 'json' });
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

    addOrUpdateDiscipline(data: any): Observable<any> {
        let url = this.baseUrl + `/api/student/add-or-update-discipline`;
        url = url.replace(/[?&]$/, "");

        const content = new FormData();
        if (data.id !== null && data.id !== undefined)
            content.append("id", data.id);
        if (data.studentId !== null && data.studentId !== undefined)
            content.append("studentId", data.studentId);
        if (data.description !== null && data.description !== undefined)
            content.append("description", data.description);
        if (data.punish !== null && data.punish !== undefined)
            content.append("punish", data.punish);

        return this.http.post<any>(url, content, { headers: this.headers, observe: 'body', responseType: 'json' } );
    }
    deleteDiscipline(id: number): Observable<any> {
        let url = this.baseUrl + `/api/student/delete-discipline?id=${id}`;
        url = url.replace(/[?&]$/, "");

        return this.http.delete<any>(url, { headers: this.headers, observe: 'body', responseType: 'json' });
    }
    getListDiscipline(keyWord: string | null | undefined, pageIndex: number, pageSize: number): Observable<PageResultBase<DisciplineDto>> {
        let url = this.baseUrl + `/api/student/get-list-discipline?Keyword=${keyWord}&PageIndex=${pageIndex}&PageSize=${pageSize}`;
        url = url.replace(/[?&]$/, "");
        return this.http.get<PageResultBase<DisciplineDto>>(url, { headers: this.headers, observe: 'body', responseType: 'json' });
    }
}
