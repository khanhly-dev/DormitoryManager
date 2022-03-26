import { mergeMap as _observableMergeMap, catchError as _observableCatch } from 'rxjs/operators';
import { Observable, throwError as _observableThrow, of as _observableOf } from 'rxjs';
import { Injectable} from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { pageResultBase } from 'src/app/dto/page-result-base';
import { areaDto } from 'src/app/dto/area-output-dto';

@Injectable({ providedIn: 'root' })
export class AreaServiceProxy {
    private baseUrl: string;

    constructor(private http: HttpClient) {
        this.baseUrl = "https://localhost:44332";
    }

    getList(keyWord: string | null | undefined, pageIndex: number, pageSize: number): Observable<pageResultBase<areaDto>> {
        let url_ = this.baseUrl + `/api/area/get-list?Keyword=${keyWord}&PageIndex=${pageIndex}&PageSize=${pageSize}`;
        url_ = url_.replace(/[?&]$/, "");

        return this.http.get<pageResultBase<areaDto>>(url_);
    }

    delete(id: number): Observable<string> {
        let url_ = this.baseUrl + `/api/area/delete?id=${id}`;
        url_ = url_.replace(/[?&]$/, "");

        return this.http.delete<string>(url_);
    }
}
