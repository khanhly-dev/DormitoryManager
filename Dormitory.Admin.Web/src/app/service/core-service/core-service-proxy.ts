import { mergeMap as _observableMergeMap, catchError as _observableCatch } from 'rxjs/operators';
import { Observable, throwError as _observableThrow, of as _observableOf } from 'rxjs';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { loginInfo } from 'src/app/dto/token';

@Injectable({providedIn : 'root'})
export class LoginServiceProxy {
    private baseUrl: string;

    constructor(private http: HttpClient) {
        this.baseUrl = "https://localhost:44345";
    }
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

    register(data: any) :Observable<any>{
        let url_ = this.baseUrl + "/api/core/register";
        url_ = url_.replace(/[?&]$/, "");

        const content_ = new FormData();
        if (data.userName !== null && data.userName !== undefined)
            content_.append("userName", data.userName);
        if (data.password !== null && data.password !== undefined)
            content_.append("password", data.password);
        if (data.tenant !== null && data.tenant !== undefined)
            content_.append("tenant", data.tenant);
        if (data.email !== null && data.email !== undefined)
            content_.append("email", data.email);
        if (data.userInfoId !== null && data.userInfoId !== undefined)
            content_.append("userInfoId", data.userInfoId);
      

        return this.http.post<any>(url_, content_);
    }
}
