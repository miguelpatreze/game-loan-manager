import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

const httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

const apiUrl = `${environment.apiUrl}/friends`;

@Injectable({
    providedIn: 'root'
})

export class FriendService {

    constructor(private http: HttpClient) { }

    post(friend): Observable<any> {
        return this.http.post<any>(apiUrl, friend, httpOptions);
    }


    getById(id): Observable<any> {
        return this.http.get<any>(`${apiUrl}/${id}`);
    }

    //   delete(id): Observable<any> {
    //     const url = `${apiUrl}/${id}`;

    //     return this.http.delete<any>(url, httpOptions);
    //   }

    get(name?, enabled?): Observable<any> {
        var params = new HttpParams();

        if (name != null) params = params.set("name", name);
        if (enabled != null) params = params.set("enabled", enabled);

        return this.http.get<any>(apiUrl, { params });
    }

    patch(friend): Observable<any> {
        return this.http.patch<any>(`${apiUrl}/${friend.id}`, friend, httpOptions);
    }

}