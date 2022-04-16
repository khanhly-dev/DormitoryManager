import { mergeMap as _observableMergeMap, catchError as _observableCatch } from 'rxjs/operators';
import { Observable, throwError as _observableThrow, of as _observableOf } from 'rxjs';
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { CriteriaDto, ExtendContractTime, StudentInfoDto } from 'src/app/dto/output-dto';

const headers = new HttpHeaders({
    "authorization": "Bearer " + localStorage.getItem('access_token') ?? "",
})

@Injectable({ providedIn: 'root' })
export class SignUpServiceProxy {
    private baseUrl: string;

    constructor(private http: HttpClient) {
        this.baseUrl = "https://localhost:44307";
    }

    getListCriteria(): Observable<CriteriaDto[]> {
        let url = this.baseUrl + `/api/contract/get-list-criteria`;
        url = url.replace(/[?&]$/, "");
        return this.http.get<CriteriaDto[]>(url, { headers: headers, observe: 'body', responseType: 'json' });
    }

    getExtendContractTime(studentId : number): Observable<ExtendContractTime> {
        let url = this.baseUrl + `/api/contract/get-extend-contract-time?studentId=${studentId}`;
        url = url.replace(/[?&]$/, "");
        return this.http.get<ExtendContractTime>(url, { headers: headers, observe: 'body', responseType: 'json' });
    }

    getSummerContractTime(studentId : number): Observable<ExtendContractTime> {
        let url = this.baseUrl + `/api/contract/get-summer-contract-time?studentId=${studentId}`;
        url = url.replace(/[?&]$/, "");
        return this.http.get<ExtendContractTime>(url, { headers: headers, observe: 'body', responseType: 'json' });
    }

    delete(id: number): Observable<any> {
        let url = this.baseUrl + `/api/contract/delete?id=${id}`;
        url = url.replace(/[?&]$/, "");

        return this.http.delete<any>(url, { headers: headers, observe: 'body', responseType: 'json' });
    }

    setStudentPoint(studentId : number, listCriteriaId : string): Observable<any> {
        let url = this.baseUrl + "/api/contract/set-student-point";
        url = url.replace(/[?&]$/, "");
        
        const content = new FormData();
        if (studentId !== null && studentId !== undefined)
            content.append("studentId", studentId.toString());
        if (listCriteriaId !== null && listCriteriaId !== undefined && listCriteriaId.length > 0)
            content.append("ListCriteriaId", listCriteriaId);

        return this.http.post<any>(url, content, { headers: headers, observe: 'body', responseType: 'json' } );
    }

    signUpDormitory(studentId : number, desiredPrice: any): Observable<any> {
        let url = this.baseUrl + "/api/contract/sgin-up";
        url = url.replace(/[?&]$/, "");

        const content = new FormData();
        if (studentId !== null && studentId !== undefined)
            content.append("studentId", studentId.toString());
        if (desiredPrice !== null && desiredPrice !== undefined)
            content.append("desiredPrice", desiredPrice);

        return this.http.post<any>(url, content, { headers: headers, observe: 'body', responseType: 'json' } );
    }

    extendContract(studentId : number): Observable<any> {
        let url = this.baseUrl + "/api/contract/extend-contract";
        url = url.replace(/[?&]$/, "");

        const content = new FormData();
        if (studentId !== null && studentId !== undefined)
            content.append("studentId", studentId.toString());

        return this.http.post<any>(url, content, { headers: headers, observe: 'body', responseType: 'json' } );
    }

    summerContract(studentId : number): Observable<any> {
        let url = this.baseUrl + "/api/contract/summer-contract";
        url = url.replace(/[?&]$/, "");

        const content = new FormData();
        if (studentId !== null && studentId !== undefined)
            content.append("studentId", studentId.toString());

        return this.http.post<any>(url, content, { headers: headers, observe: 'body', responseType: 'json' } );
    }

    studentConfirm(contractId : any, confirmStatus: any): Observable<any> {
        let url = this.baseUrl + "/api/contract/student-confirm";
        url = url.replace(/[?&]$/, "");

        const content = new FormData();
        if (contractId !== null && contractId !== undefined)
            content.append("contractId", contractId.toString());
        if (confirmStatus !== null && confirmStatus !== undefined)
            content.append("confirmStatus", confirmStatus);

        return this.http.put<any>(url, content, { headers: headers, observe: 'body', responseType: 'json' } );
    }
}
