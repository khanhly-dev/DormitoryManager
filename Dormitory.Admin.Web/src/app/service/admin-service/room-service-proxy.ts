import { mergeMap as _observableMergeMap, catchError as _observableCatch } from 'rxjs/operators';
import { Observable, throwError as _observableThrow, of as _observableOf } from 'rxjs';
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { PageResultBase } from 'src/app/dto/page-result-base';
import { BaseSelectDto, BillServiceDto, RoomDto, RoomSelectDto } from 'src/app/dto/output-dto';

@Injectable({ providedIn: 'root' })
export class RoomServiceProxy {
    private baseUrl: string;
    private headers!: HttpHeaders

    constructor(private http: HttpClient) {
        this.baseUrl = "https://localhost:44332";
        this.headers = new HttpHeaders({
            "authorization": "Bearer " + localStorage.getItem('access_token') ?? "",
        })
    }
    getList(keyWord: string | null | undefined, pageIndex: number, pageSize: number): Observable<PageResultBase<RoomDto>> {
        let url = this.baseUrl + `/api/room/get-list?Keyword=${keyWord}&PageIndex=${pageIndex}&PageSize=${pageSize}`;
        url = url.replace(/[?&]$/, "");
        return this.http.get<PageResultBase<RoomDto>>(url, { headers: this.headers, observe: 'body', responseType: 'json' });
    }

    getListBill(roomId : any): Observable<BillServiceDto[]> {
        let url = this.baseUrl + `/api/room/get-list-bill-by-room?roomId=${roomId}`;
        url = url.replace(/[?&]$/, "");
        return this.http.get<BillServiceDto[]>(url, { headers: this.headers, observe: 'body', responseType: 'json' });
    }

    getListSelect(): Observable<BaseSelectDto[]> {
        let url = this.baseUrl + `/api/room/get-list-select`;
        url = url.replace(/[?&]$/, "");
        return this.http.get<BaseSelectDto[]>(url, { headers: this.headers, observe: 'body', responseType: 'json' });
    }

    getListEmptyRoom(): Observable<RoomSelectDto[]> {
        let url = this.baseUrl + `/api/room/get-list-empty-room`;
        url = url.replace(/[?&]$/, "");
        return this.http.get<RoomSelectDto[]>(url, { headers: this.headers, observe: 'body', responseType: 'json' });
    }

    delete(id: number): Observable<any> {
        let url = this.baseUrl + `/api/room/delete?id=${id}`;
        url = url.replace(/[?&]$/, "");

        return this.http.delete<any>(url, { headers: this.headers, observe: 'body', responseType: 'json' });
    }

    createOrUpdate(data: any): Observable<any> {
        let url = this.baseUrl + "/api/room/create-or-update";
        url = url.replace(/[?&]$/, "");

        const content = new FormData();
        if (data.id !== null && data.id !== undefined)
            content.append("id", data.id);
        if (data.name !== null && data.name !== undefined)
            content.append("name", data.name.toString());
        if (data.price !== null || data.price !== undefined)
            content.append("price", data.price);
        if (data.areaId !== null || data.areaId !== undefined)
            content.append("areaId", data.areaId);
        if (data.maxSlot !== null || data.maxSlot !== undefined)
            content.append("maxSlot", data.maxSlot);
        if (data.minSlot !== null || data.minSlot !== undefined)
            content.append("price", data.minSlot);
        if (data.filledSlot !== null || data.filledSlot !== undefined)
            content.append("filledSlot", data.filledSlot);
        if (data.avaiableSlot !== null || data.avaiableSlot !== undefined)
            content.append("avaiableSlot", data.avaiableSlot);

        return this.http.post<any>(url, content, { headers: this.headers, observe: 'body', responseType: 'json' } );
    }
}
