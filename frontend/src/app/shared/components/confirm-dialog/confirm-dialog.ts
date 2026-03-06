import { ChangeDetectionStrategy, Component, HostListener, inject } from '@angular/core';
import { LucideAngularModule } from 'lucide-angular';
import { ConfirmDialogService } from './confirm-dialog.service';

@Component({
  selector: 'app-confirm-dialog',
  imports: [LucideAngularModule],
  templateUrl: './confirm-dialog.html',
  styleUrl: './confirm-dialog.css',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class ConfirmDialogComponent {
  protected readonly service = inject(ConfirmDialogService);
  protected readonly state = this.service.state;

  @HostListener('document:keydown.escape')
  onEscape(): void {
    if (this.state()) this.service.resolve(false);
  }

  protected confirm(): void {
    this.service.resolve(true);
  }

  protected cancel(): void {
    this.service.resolve(false);
  }
}
