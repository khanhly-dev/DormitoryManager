import { mergeMap as _observableMergeMap, catchError as _observableCatch } from 'rxjs/operators';
import { Observable, throwError as _observableThrow, of as _observableOf } from 'rxjs';
import { Injectable, Inject, Optional, InjectionToken } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { loginInfo } from 'src/app/Dto/token';

export const API_BASE_URL = new InjectionToken<string>('API_BASE_URL');

@Injectable({providedIn : 'root'})
export class LoginServiceProxy {
    private http: HttpClient;
    private baseUrl: string;
    protected jsonParseReviver: ((key: string, value: any) => any) | undefined = undefined;

    constructor(@Inject(HttpClient) http: HttpClient, @Optional() @Inject(API_BASE_URL) baseUrl?: string) {
        this.http = http;
        this.baseUrl = "https://localhost:44345";
    }

    /**
     * @param userName (optional) 
     * @param password (optional) 
     * @param tenantId (optional) 
     * @return Success
     */
    login(userName: string | null | undefined, password: string | null | undefined, tenantId: number | undefined) :Observable<loginInfo>{
        let url_ = this.baseUrl + "/api/core/login";
        url_ = url_.replace(/[?&]$/, "");

        const content_ = new FormData();
        if (userName !== null && userName !== undefined)
            content_.append("userName", userName.toString());
        if (password !== null && password !== undefined)
            content_.append("password", password.toString());
        if (tenantId === null || tenantId === undefined)
            throw new Error("The parameter 'tenantId' cannot be null.");
        else
            content_.append("tenantId", tenantId.toString());

        return this.http.post<loginInfo>(url_, content_);
    }
}
