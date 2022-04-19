import { mergeMap as _observableMergeMap, catchError as _observableCatch } from 'rxjs/operators';
import { Observable, throwError as _observableThrow, of as _observableOf } from 'rxjs';
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { PageResultBase } from 'src/app/dto/page-result-base';
import { AreaDto, BaseStatDto } from 'src/app/dto/output-dto';

@Injectable({ providedIn: 'root' })
export class DashboardServiceProxy {
   
    private baseUrl: string;
    private headers!: HttpHeaders

    constructor(private http: HttpClient) {
        this.baseUrl = "https://localhost:44332";
        this.headers = new HttpHeaders({
            "authorization": "Bearer " + localStorage.getItem('access_token') ?? "",
        })
    }

    getBaseStat(): Observable<BaseStatDto> {
        let url = this.baseUrl + `/api/dashboard/get-total-stat`;
        url = url.replace(/[?&]$/, "");
        return this.http.get<BaseStatDto>(url, { headers: this.headers, observe: 'body', responseType: 'json' });
    }
}
