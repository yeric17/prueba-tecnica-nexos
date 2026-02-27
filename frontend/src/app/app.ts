import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet],
  templateUrl: './app.html',
  styleUrls: ['./app.css','./theme/variables.css','./theme/reset.css','./theme/typography.css','./theme/forms.css']
})
export class App {
  protected readonly title = signal('nexos');
}
