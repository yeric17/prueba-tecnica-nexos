import { ChangeDetectionStrategy, Component } from '@angular/core';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-orders-list-page',
  imports: [RouterLink],
  templateUrl: './orders-list-page.html',
  styleUrl: './orders-list-page.css',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class OrdersListPage {

}
