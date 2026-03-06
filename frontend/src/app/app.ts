import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { ConfirmDialogComponent } from './shared/components/confirm-dialog/confirm-dialog';
import { ToastContainerComponent } from './shared/components/toast/toast-container';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, ConfirmDialogComponent, ToastContainerComponent],
  templateUrl: './app.html',
  styleUrls: ['./app.css','./theme/variables.css','./theme/reset.css','./theme/typography.css','./theme/forms.css']
})
export class App {
  protected readonly title = signal('nexos');
}
