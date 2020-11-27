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

    //   post(operationalPerson): Observable<any> {
    //     return this.http.post<any>(apiUrl, operationalPerson, httpOptions);
    //   }
    //   patch(id, operationalPerson): Observable<any> {
    //     return this.http.patch<any>(`${apiUrl}/${id}`, operationalPerson, httpOptions);
    //   }

    //   get(firstName?, lastName?, email?, taxId?, pageNumber?) : Observable<any> {
    //     var params = new HttpParams();

    //     if (firstName != null) params = params.set("firstName", firstName);
    //     if (lastName != null) params = params.append("lastName", lastName);
    //     if (email != null) params = params.append("email", email);
    //     if (taxId != null) params = params.append("taxId", taxId);
    //     if (pageNumber != null) params = params.append("pageNumber", pageNumber);

    //     return this.http.get<any>(apiUrl, { params });
    //   }

    getById(id): Observable<any> {
        return this.http.get<any>(`${apiUrl}/${id}`);
    }

    //   delete(id): Observable<any> {
    //     const url = `${apiUrl}/${id}`;

    //     return this.http.delete<any>(url, httpOptions);
    //   }

    get(name?, enabled?): Observable<any>  {
        var params = new HttpParams();

        if (name != null) params = params.set("name", name);
        if (enabled != null) params = params.set("enabled", enabled);

        return this.http.get<any>(apiUrl, { params });
    }
}