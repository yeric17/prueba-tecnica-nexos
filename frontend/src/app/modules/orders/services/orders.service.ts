import { HttpClient } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";
import { environment } from "../../../../environments/environment";
import { Order } from "../models/orders.model";
import { AuthService } from "../../auth/services/auth.service";

@Injectable({
  providedIn: 'root'
})
export class OrdersService {
    private readonly http = inject(HttpClient)
    private readonly apiHost = environment.apiHost;
    private readonly user = inject(AuthService).user;

    getOrders() {
        return this.http.get<Order[]>(`${this.apiHost}/orders-service/orders/user/${this.user()?.id}`);
    }
}