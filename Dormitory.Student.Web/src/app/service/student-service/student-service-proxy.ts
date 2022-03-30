import { mergeMap as _observableMergeMap, catchError as _observableCatch } from 'rxjs/operators';
import { Observable, throwError as _observableThrow, of as _observableOf } from 'rxjs';
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { StudentInfoDto } from 'src/app/dto/output-dto';

const headers = new HttpHeaders({
    "authorization": "Bearer " + localStorage.getItem('access_token') ?? "",
})

@Injectable({ providedIn: 'root' })
export class StudentServiceProxy {
    private baseUrl: string;

    constructor(private http: HttpClient) {
        this.baseUrl = "https://localhost:44307";
    }

    getStudentByUserId(userId: number): Observable<StudentInfoDto> {
        let url = this.baseUrl + `/api/student/get-student-info?userId=${userId}`;
        url = url.replace(/[?&]$/, "");
        return this.http.get<StudentInfoDto>(url, { headers: headers, observe: 'body', responseType: 'json' });
    }
}
