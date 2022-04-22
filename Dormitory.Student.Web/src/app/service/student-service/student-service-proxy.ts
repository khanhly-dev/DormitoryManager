import { mergeMap as _observableMergeMap, catchError as _observableCatch } from 'rxjs/operators';
import { Observable, throwError as _observableThrow, of as _observableOf } from 'rxjs';
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { ContractPendingDto, DisciplineDto, StudentInfoDto } from 'src/app/dto/output-dto';
import { PageResultBase } from 'src/app/dto/page-result-base';

const headers = new HttpHeaders({
    "authorization": "Bearer " + localStorage.getItem('access_token') ?? "",
})

@Injectable({ providedIn: 'root' })
export class StudentServiceProxy {
    private baseUrl: string;

    constructor(private http: HttpClient) {
        this.baseUrl = "https://localhost:44307";
    }

    getStudentByUserId(userId: number): Observable<StudentInfoDto> {
        let url = this.baseUrl + `/api/student/get-student-info?userId=${userId}`;
        url = url.replace(/[?&]$/, "");
        return this.http.get<StudentInfoDto>(url, { headers: headers, observe: 'body', responseType: 'json' });
    }
    checkSignUpStatus(studentId: number): Observable<boolean> {
        let url = this.baseUrl + `/api/student/check-sign-up-status?studentId=${studentId}`;
        url = url.replace(/[?&]$/, "");
        return this.http.get<boolean>(url, { headers: headers, observe: 'body', responseType: 'json' });
    }
    checkCreateExtendContract(studentId: number): Observable<boolean> {
        let url = this.baseUrl + `/api/student/check-create-extend-contract?studentId=${studentId}`;
        url = url.replace(/[?&]$/, "");
        return this.http.get<boolean>(url, { headers: headers, observe: 'body', responseType: 'json' });
    }
    checkCreateSummerContract(studentId: number): Observable<boolean> {
        let url = this.baseUrl + `/api/student/check-create-summer-contract?studentId=${studentId}`;
        url = url.replace(/[?&]$/, "");
        return this.http.get<boolean>(url, { headers: headers, observe: 'body', responseType: 'json' });
    }
    getRecomendPrice(): Observable<number[]> {
        let url = this.baseUrl + `/api/student/get-room-price-range`;
        url = url.replace(/[?&]$/, "");
        return this.http.get<number[]>(url, { headers: headers, observe: 'body', responseType: 'json' });
    }
    getListStudentConfirmContract(keyWord: string, studentId : number, pageIndex: number, pageSize: number): Observable<PageResultBase<ContractPendingDto>> {
        let url = this.baseUrl + `/api/student/get-list-student-confirm-contract?keyword=${keyWord}&studentId=${studentId}&pageIndex=${pageIndex}&pageSize=${pageSize}`;
        url = url.replace(/[?&]$/, "");
        return this.http.get<PageResultBase<ContractPendingDto>>(url, { headers: headers, observe: 'body', responseType: 'json' });
    }
    getListDiscipline(keyWord: string | null | undefined, pageIndex: number, pageSize: number): Observable<PageResultBase<DisciplineDto>> {
        let url = 'https://localhost:44332' + `/api/student/get-list-discipline?Keyword=${keyWord}&PageIndex=${pageIndex}&PageSize=${pageSize}`;
        url = url.replace(/[?&]$/, "");
        return this.http.get<PageResultBase<DisciplineDto>>(url, { headers: headers, observe: 'body', responseType: 'json' });
    }
}
