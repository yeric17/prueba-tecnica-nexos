import { inject, Injectable, signal } from '@angular/core';
import { LoginRequest, LoginResponse, UserResponse } from '../models/login.model';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../../environments/environment';
import { firstValueFrom, tap } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class AuthService {

    private API_HOST = environment.apiHost
    private AUTH_TOKEN_STORE_KEY = environment.authTokenKey;

    private http = inject(HttpClient)

    private _user = signal<UserResponse|undefined>(undefined)

    public user = this._user.asReadonly()

    login(request:LoginRequest) {
        return this.http.
        post<LoginResponse>(`${this.API_HOST}/users-service/users/login`,request)
        .pipe(
            tap((response)=>{
                this.storeToken(response)
            })
        )
    }

    storeToken(response:LoginResponse){
        localStorage.setItem(this.AUTH_TOKEN_STORE_KEY,JSON.stringify(response))
    }


    getToken(){
        const token = localStorage.getItem(this.AUTH_TOKEN_STORE_KEY)
        if(token === null) return null
        const tokenObj:LoginResponse = JSON.parse(token)
        return tokenObj
    }

    logout(): void {
        localStorage.removeItem(this.AUTH_TOKEN_STORE_KEY)
        this._user.set(undefined)
        window.location.reload()
    }

    getMe(){
        return this.http.get<UserResponse>(`${this.API_HOST}/users-service/users/me`)
        .pipe(
            tap(res =>{
                if(res) this._user.set(res)
            })
        )
    }

    async isAuthenticated() {
        try {
            
            await firstValueFrom(this.getMe())
            return true
        } catch (error) {
            return false
        }

    }
}