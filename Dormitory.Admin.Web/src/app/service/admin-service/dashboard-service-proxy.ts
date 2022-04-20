import { mergeMap as _observableMergeMap, catchError as _observableCatch } from 'rxjs/operators';
import { Observable, throwError as _observableThrow, of as _observableOf } from 'rxjs';
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { PageResultBase } from 'src/app/dto/page-result-base';
import { AreaChartDto, AreaDto, BaseStatDto, feeChartDto, FeeDto, GenderPercent } from 'src/app/dto/output-dto';

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
    getListServiceFee(keyWord: string | null | undefined, pageIndex: number, pageSize: number): Observable<PageResultBase<FeeDto>> {
        let url = this.baseUrl + `/api/dashboard/get-list-service-fee?Keyword=${keyWord}&PageIndex=${pageIndex}&PageSize=${pageSize}`;
        url = url.replace(/[?&]$/, "");
        return this.http.get<PageResultBase<FeeDto>>(url, { headers: this.headers, observe: 'body', responseType: 'json' });
    }
    getListContractFee(keyWord: string | null | undefined, pageIndex: number, pageSize: number): Observable<PageResultBase<FeeDto>> {
        let url = this.baseUrl + `/api/dashboard/get-list-contract-fee?Keyword=${keyWord}&PageIndex=${pageIndex}&PageSize=${pageSize}`;
        url = url.replace(/[?&]$/, "");
        return this.http.get<PageResultBase<FeeDto>>(url, { headers: this.headers, observe: 'body', responseType: 'json' });
    }
    getGenderPercent(): Observable<GenderPercent> {
        let url = this.baseUrl + `/api/dashboard/get-gender-percent`;
        url = url.replace(/[?&]$/, "");
        return this.http.get<GenderPercent>(url, { headers: this.headers, observe: 'body', responseType: 'json' });
    }
    getContractFeeChart(): Observable<feeChartDto> {
        let url = this.baseUrl + `/api/dashboard/get-contract-fee-chart`;
        url = url.replace(/[?&]$/, "");
        return this.http.get<feeChartDto>(url, { headers: this.headers, observe: 'body', responseType: 'json' });
    }
    getServiceFeeChart(): Observable<feeChartDto> {
        let url = this.baseUrl + `/api/dashboard/get-service-fee-chart`;
        url = url.replace(/[?&]$/, "");
        return this.http.get<feeChartDto>(url, { headers: this.headers, observe: 'body', responseType: 'json' });
    }
    getAreaChart(): Observable<AreaChartDto[]> {
        let url = this.baseUrl + `/api/dashboard/get-area-chart`;
        url = url.replace(/[?&]$/, "");
        return this.http.get<AreaChartDto[]>(url, { headers: this.headers, observe: 'body', responseType: 'json' });
    }
}
