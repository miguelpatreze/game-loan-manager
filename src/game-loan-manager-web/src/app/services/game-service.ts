import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { environment } from 'src/environments/environment';

const httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

const apiUrl = `${environment.apiUrl}/games`;

@Injectable({
    providedIn: 'root'
})

export class GameService {

    constructor(private http: HttpClient) { }

    post(game): Observable<any> {
        return this.http.post<any>(apiUrl, game, httpOptions);
    }

    getById(id): Observable<any> {
        return this.http.get<any>(`${apiUrl}/${id}`);
    }

    delete(id): Observable<any> {
        return this.http.delete<any>(`${apiUrl}/${id}`, httpOptions);
    }

    get(name?, enabled?): Observable<any> {
        var params = new HttpParams();

        if (name != null) params = params.set("name", name);
        if (enabled != null) params = params.set("enabled", enabled);

        return this.http.get<any>(apiUrl, { params });
    }

    patch(game): Observable<any> {
        return this.http.patch<any>(`${apiUrl}/${game.id}`, game, httpOptions);
    }

    postLoan(loan): Observable<any> {
        return this.http.post<any>(`${apiUrl}/${loan.gameId}/loans/${loan.friendId}`, httpOptions);
    }
    postDevolution(devolution): Observable<any> {
        return this.http.post<any>(`${apiUrl}/${devolution.gameId}/devolutions`, httpOptions);
    }
}