import { HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Injectable } from '@angular/core';
import { InformationDialogService } from '../shared/components/dialogs/information-dialog/information-dialog/information-dialog.service';

@Injectable()
export class HttpErrorInterceptor implements HttpInterceptor {

    constructor(
        private informationDialogService: InformationDialogService
    ) { }

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<any> {
        return next.handle(request)
            .pipe(
                catchError((httpErrorResponse: HttpErrorResponse) => {
                    if (!httpErrorResponse || !httpErrorResponse.error)
                        return throwError('unexpected error');

                    if (httpErrorResponse.status == 500) {
                        this.informationDialogService.open({
                            message: 'Ocorreu um erro inesperado, por favor tente novamente mais tarde.',
                            messageType: 'error'
                        });

                        return throwError("unexpected error");
                    }

                    if (httpErrorResponse.status == 400) {
                        if (httpErrorResponse.error.notifications) {
                            this.informationDialogService.open({
                                message: httpErrorResponse.error.notifications.map((v, i, array) => v.errorMessage).join('\n'),
                                messageType: 'error'
                            });
                        }

                        return throwError("validation error");
                    }
                    
                    return throwError("an error has ocurred");

                })
            )
    }
}