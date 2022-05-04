import { mergeMap as _observableMergeMap, catchError as _observableCatch } from 'rxjs/operators';
import { Observable, throwError as _observableThrow, of as _observableOf } from 'rxjs';
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { PageResultBase } from 'src/app/dto/page-result-base';
import { BaseSelectDto, DisciplineDto, StudentDto, UserDto, UserInfoDto } from 'src/app/dto/output-dto';

@Injectable({ providedIn: 'root' })
export class UserServiceProxy {
   
    private baseUrl: string;
    private headers!: HttpHeaders

    constructor(private http: HttpClient) {
        this.baseUrl = "https://localhost:44332";
        this.headers = new HttpHeaders({
            "authorization": "Bearer " + localStorage.getItem('access_token') ?? "",
        })
    }

    getList(keyWord: string | null | undefined, pageIndex: number, pageSize: number): Observable<PageResultBase<UserInfoDto>> {
        let url = this.baseUrl + `/api/user/get-list?Keyword=${keyWord}&PageIndex=${pageIndex}&PageSize=${pageSize}`;
        url = url.replace(/[?&]$/, "");
        return this.http.get<PageResultBase<UserInfoDto>>(url, { headers: this.headers, observe: 'body', responseType: 'json' });
    }

    getAccountByUser(userInfoId : number, tenant: number): Observable<UserDto> {
        let url = this.baseUrl + `/api/user/get-by-user?userInfoId=${userInfoId}&tenant=${tenant}`;
        url = url.replace(/[?&]$/, "");
        return this.http.get<UserDto>(url, { headers: this.headers, observe: 'body', responseType: 'json' });
    }

    addOrUpdateUser(data: any): Observable<any> {
        let url = this.baseUrl + `/api/user/create-or-update`;
        url = url.replace(/[?&]$/, "");

        const content = new FormData();
        if (data.id !== null && data.id !== undefined)
            content.append("id", data.id);
        if (data.name !== null && data.name !== undefined)
            content.append("name", data.name);
        if (data.phone !== null && data.phone !== undefined)
            content.append("phone", data.phone);
        if (data.dob !== null && data.dob !== undefined)
            content.append("dob", data.dob);
        if (data.position !== null && data.position !== undefined)
            content.append("position", data.position);
        if (data.adress !== null && data.adress !== undefined)
            content.append("adress", data.adress);
        if (data.gender !== null && data.gender !== undefined)
            content.append("gender", data.gender);


        return this.http.post<any>(url, content, { headers: this.headers, observe: 'body', responseType: 'json' } );
    }
    deleteUser(id: number): Observable<any> {
        let url = this.baseUrl + `/api/user/delete?id=${id}`;
        url = url.replace(/[?&]$/, "");

        return this.http.delete<any>(url, { headers: this.headers, observe: 'body', responseType: 'json' });
    }
    deleteAccount(id: number): Observable<any> {
        let url = this.baseUrl + `/api/user/delete-account?id=${id}`;
        url = url.replace(/[?&]$/, "");

        return this.http.delete<any>(url, { headers: this.headers, observe: 'body', responseType: 'json' });
    }
}
