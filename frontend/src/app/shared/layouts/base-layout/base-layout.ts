import { ChangeDetectionStrategy, Component } from '@angular/core';
import { Header } from "../header/header";
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-base-layout',
  imports: [Header, RouterOutlet],
  templateUrl: './base-layout.html',
  styleUrl: './base-layout.css',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class BaseLayout {

}
