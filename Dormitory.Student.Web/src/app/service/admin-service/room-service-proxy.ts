import { mergeMap as _observableMergeMap, catchError as _observableCatch } from 'rxjs/operators';
import { Observable, throwError as _observableThrow, of as _observableOf } from 'rxjs';
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { PageResultBase } from 'src/app/dto/page-result-base';
import { RoomDto } from 'src/app/dto/output-dto';

const headers = new HttpHeaders({
    "authorization": "Bearer " + localStorage.getItem('access_token') ?? "",
})

@Injectable({ providedIn: 'root' })
export class RoomServiceProxy {
    private baseUrl: string;

    constructor(private http: HttpClient) {
        this.baseUrl = "https://localhost:44332";
    }

    getList(keyWord: string | null | undefined, pageIndex: number, pageSize: number): Observable<PageResultBase<RoomDto>> {
        let url = this.baseUrl + `/api/room/get-list?Keyword=${keyWord}&PageIndex=${pageIndex}&PageSize=${pageSize}`;
        url = url.replace(/[?&]$/, "");
        return this.http.get<PageResultBase<RoomDto>>(url, { headers: headers, observe: 'body', responseType: 'json' });
    }

    delete(id: number): Observable<any> {
        let url = this.baseUrl + `/api/room/delete?id=${id}`;
        url = url.replace(/[?&]$/, "");

        return this.http.delete<any>(url, { headers: headers, observe: 'body', responseType: 'json' });
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
            content.append("price", data.filledSlot);

        return this.http.post<any>(url, content, { headers: headers, observe: 'body', responseType: 'json' } );
    }
}
