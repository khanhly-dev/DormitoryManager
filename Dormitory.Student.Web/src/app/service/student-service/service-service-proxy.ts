import { mergeMap as _observableMergeMap, catchError as _observableCatch } from 'rxjs/operators';
import { Observable, throwError as _observableThrow, of as _observableOf } from 'rxjs';
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { PageResultBase } from 'src/app/dto/page-result-base';
import { BaseSelectDto, FacilityDto, RoomDto, RoomServiceDto, ServiceDto } from 'src/app/dto/output-dto';

@Injectable({ providedIn: 'root' })
export class ServiceServiceProxy {
    private baseUrl: string;
    private headers!: HttpHeaders

    constructor(private http: HttpClient) {
        this.baseUrl = "https://localhost:44332";
        this.headers = new HttpHeaders({
            "authorization": "Bearer " + localStorage.getItem('access_token') ?? "",
        })
    }

    getList(keyWord: string | null | undefined, pageIndex: number, pageSize: number): Observable<PageResultBase<ServiceDto>> {
        let url = this.baseUrl + `/api/service/get-list?Keyword=${keyWord}&PageIndex=${pageIndex}&PageSize=${pageSize}`;
        url = url.replace(/[?&]$/, "");
        return this.http.get<PageResultBase<ServiceDto>>(url, { headers: this.headers, observe: 'body', responseType: 'json' });
    }

    getListSelect(): Observable<BaseSelectDto[]> {
        let url = this.baseUrl + `/api/service/get-list-select`;
        url = url.replace(/[?&]$/, "");
        return this.http.get<BaseSelectDto[]>(url, { headers: this.headers, observe: 'body', responseType: 'json' });
    }

    getServiceByRoom(roomId : any): Observable<RoomServiceDto[]> {
        let url = this.baseUrl + `/api/service/get-service-in-room?roomId=${roomId}`;
        url = url.replace(/[?&]$/, "");
        return this.http.get<RoomServiceDto[]>(url, { headers: this.headers, observe: 'body', responseType: 'json' });
    }

    getServiceByBill(billId : any): Observable<RoomServiceDto[]> {
        let url = this.baseUrl + `/api/service/get-service-in-bill?billId=${billId}`;
        url = url.replace(/[?&]$/, "");
        return this.http.get<RoomServiceDto[]>(url, { headers: this.headers, observe: 'body', responseType: 'json' });
    }

    delete(id: number): Observable<any> {
        let url = this.baseUrl + `/api/service/delete?id=${id}`;
        url = url.replace(/[?&]$/, "");

        return this.http.delete<any>(url, { headers: this.headers, observe: 'body', responseType: 'json' });
    }

    deleteBillService(billId: number): Observable<any> {
        let url = this.baseUrl + `/api/service/delete-bill-service?billId=${billId}`;
        url = url.replace(/[?&]$/, "");

        return this.http.delete<any>(url, { headers: this.headers, observe: 'body', responseType: 'json' });
    }

    deleteRoomService(roomService: number): Observable<any> {
        let url = this.baseUrl + `/api/service/delete-room-service?roomServiceId=${roomService}`;
        url = url.replace(/[?&]$/, "");

        return this.http.delete<any>(url, { headers: this.headers, observe: 'body', responseType: 'json' });
    }

    updateServicePaid(roomServiceId: any, data: any): Observable<any> {
        let url = this.baseUrl + `/api/service/update-room-service-fee`;
        url = url.replace(/[?&]$/, "");

        const content = new FormData();
        if (roomServiceId !== null && roomServiceId !== undefined)
            content.append("roomServiceId", roomServiceId);
        if (data.datePaid !== null && data.datePaid !== undefined)
            content.append("datePaid", data.datePaid);
        if (data.moneyPaid !== null && data.moneyPaid !== undefined)
            content.append("moneyPaid", data.moneyPaid.toString());

        return this.http.put<any>(url, content, { headers: this.headers, observe: 'body', responseType: 'json' } );
    }

    createOrUpdate(data: any): Observable<any> {
        let url = this.baseUrl + "/api/service/create-or-update";
        url = url.replace(/[?&]$/, "");

        const content = new FormData();
        if (data.id !== null && data.id !== undefined)
            content.append("id", data.id);
        if (data.name !== null && data.name !== undefined)
            content.append("name", data.name.toString());
        if (data.price !== null || data.price !== undefined)
            content.append("price", data.price);
        if (data.unit !== null || data.unit !== undefined)
            content.append("unit", data.unit);
        if (data.serviceType !== null || data.serviceType !== undefined)
            content.append("serviceType", data.serviceType);

        return this.http.post<any>(url, content, { headers: this.headers, observe: 'body', responseType: 'json' } );
    }

    addServiceForRoom(data: any, roomId: any, fromDate: any, toDate: any): Observable<any> {
        let url = this.baseUrl + "/api/service/add-service-for-room";
        url = url.replace(/[?&]$/, "");

        const content = new FormData();
       
        if (data !== null && data !== undefined)
            content.append("request", data);
        if (roomId !== null && roomId !== undefined)
            content.append("roomId", roomId);
        if (fromDate !== null && fromDate !== undefined)
            content.append("fromDate", fromDate);
        if (toDate !== null && toDate !== undefined)
            content.append("toDate", toDate);

        return this.http.post<any>(url, content, { headers: this.headers, observe: 'body', responseType: 'json' } );
    }
}
