import { mergeMap as _observableMergeMap, catchError as _observableCatch } from 'rxjs/operators';
import { Observable, throwError as _observableThrow, of as _observableOf } from 'rxjs';
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { PageResultBase } from 'src/app/dto/page-result-base';
import { BaseSelectDto, FacilityDto, FacilityInRoom, RoomDto } from 'src/app/dto/output-dto';

@Injectable({ providedIn: 'root' })
export class FacilityServiceProxy {
    private baseUrl: string;
    private headers!: HttpHeaders

    constructor(private http: HttpClient) {
        this.baseUrl = "https://localhost:44332";
        this.headers = new HttpHeaders({
            "authorization": "Bearer " + localStorage.getItem('access_token') ?? "",
        })
    }

    getList(keyWord: string | null | undefined, pageIndex: number, pageSize: number): Observable<PageResultBase<FacilityDto>> {
        let url = this.baseUrl + `/api/facility/get-list?Keyword=${keyWord}&PageIndex=${pageIndex}&PageSize=${pageSize}`;
        url = url.replace(/[?&]$/, "");
        return this.http.get<PageResultBase<FacilityDto>>(url, { headers: this.headers, observe: 'body', responseType: 'json' });
    }

    delete(id: number): Observable<any> {
        let url = this.baseUrl + `/api/facility/delete?id=${id}`;
        url = url.replace(/[?&]$/, "");

        return this.http.delete<any>(url, { headers: this.headers, observe: 'body', responseType: 'json' });
    }

    createOrUpdate(data: any): Observable<any> {
        let url = this.baseUrl + "/api/facility/create-or-update";
        url = url.replace(/[?&]$/, "");

        const content = new FormData();
        if (data.id !== null && data.id !== undefined)
            content.append("id", data.id);
        if (data.name !== null && data.name !== undefined)
            content.append("name", data.name.toString());
        if (data.totalCount !== null || data.totalCount !== undefined)
            content.append("totalCount", data.totalCount);
        if (data.status !== null || data.status !== undefined)
            content.append("status", data.status);

        return this.http.post<any>(url, content, { headers: this.headers, observe: 'body', responseType: 'json' } );
    }

    getListFacilityInRoom(): Observable<PageResultBase<FacilityInRoom>> {
        let url = this.baseUrl + `/api/facility/get-list-facility-in-room`;
        url = url.replace(/[?&]$/, "");
        return this.http.get<PageResultBase<FacilityInRoom>>(url, { headers: this.headers, observe: 'body', responseType: 'json' });
    }

    getListFacilitySelect(): Observable<BaseSelectDto[]> {
        let url = this.baseUrl + `/api/facility/get-list-select`;
        url = url.replace(/[?&]$/, "");
        return this.http.get<BaseSelectDto[]>(url, { headers: this.headers, observe: 'body', responseType: 'json' });
    }

    getListFacilityByRoomId(roomId: number): Observable<FacilityInRoom[]> {
        let url = this.baseUrl + `/api/facility/get-list-facility-by-room?roomId=${roomId}`;
        url = url.replace(/[?&]$/, "");
        return this.http.get<FacilityInRoom[]>(url, { headers: this.headers, observe: 'body', responseType: 'json' });
    }

    deleteFacilityInRoom(id: number): Observable<any> {
        let url = this.baseUrl + `/api/facility/delete-facility-in-room?id=${id}`;
        url = url.replace(/[?&]$/, "");

        return this.http.delete<any>(url, { headers: this.headers, observe: 'body', responseType: 'json' });
    }

    addFacilityIntoRoom(data: any): Observable<any> {
        debugger
        let url = this.baseUrl + "/api/facility/add-facility-into-room";
        url = url.replace(/[?&]$/, "");

        const content = new FormData();
        if (data.id !== null && data.id !== undefined)
            content.append("id", data.id);
        if (data.roomId !== null && data.roomId !== undefined)
            content.append("roomId", data.roomId.toString());
        if (data.facilityId !== null || data.facilityId !== undefined)
            content.append("facilityId", data.facilityId);
        if (data.count !== null || data.count !== undefined)
            content.append("count", data.count);
        if (data.status !== null || data.status !== undefined)
            content.append("status", data.status);

        return this.http.post<any>(url, content, { headers: this.headers, observe: 'body', responseType: 'json' } );
    }
}
