import { mergeMap as _observableMergeMap, catchError as _observableCatch } from 'rxjs/operators';
import { Observable, throwError as _observableThrow, of as _observableOf } from 'rxjs';
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { CriteriaDto, StudentInfoDto } from 'src/app/dto/output-dto';

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
}
